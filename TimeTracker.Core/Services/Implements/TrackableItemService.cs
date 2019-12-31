using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate.Criterion;
using NHibernate.Linq;
using TimeTracker.Core.Data;
using TimeTracker.Core.Models;
using TimeTracker.Core.Models.Utils;

namespace TimeTracker.Core.Services.Implements
{
    public class TrackableItemService : ABaseService<TrackableItem>, ITrackableItemService
    {
        public TrackableItemService(INHibernateHelper database) : base(database)
        {

        }

        public bool CheckAvailableCountBetweenTimes(int id, DateTime from, DateTime to, int count = 1)
        {
            return this.CheckAvailableCountBetweenTimes(new int[] { id }, from, to, count);
        }

        public bool CheckAvailableCountBetweenTimes(int[] ids, DateTime from, DateTime to, int count = 1)
        {
            TrackableItemStateItem trackableItemStateItemAlias = null;
            TrackableItem trackableItemAlias = null;


            using (var sess = this.Database.getSession())
            {
                var items = sess.QueryOver<TrackableItem>(() => trackableItemAlias)
                .WhereRestrictionOn(() => trackableItemAlias.Id).IsIn(ids)
                .List();

                var map = new List<AvailableCountMap>();

                //var states=new 
                var states = sess.QueryOver<TrackableItemStateItem>(() => trackableItemStateItemAlias)
                .WhereRestrictionOn(() => trackableItemStateItemAlias.Item.Id).IsIn(ids)
                .And(() => trackableItemStateItemAlias.Deleted != true)
                .And(Restrictions.Disjunction()
                    .Add(() => trackableItemStateItemAlias.Start <= from && trackableItemStateItemAlias.Blocked != true)   //not ended
                    .Add(() => trackableItemStateItemAlias.Start <= to && trackableItemStateItemAlias.Blocked != true)     //not started

                    .Add(() => trackableItemStateItemAlias.Start >= from && trackableItemStateItemAlias.Finish < to)       //inner
                    .Add(() => trackableItemStateItemAlias.Start <= from && trackableItemStateItemAlias.Finish >= from)    //outer
                    .Add(() => trackableItemStateItemAlias.Start <= from && trackableItemStateItemAlias.Finish > from)     //left
                    .Add(() => trackableItemStateItemAlias.Start < to && trackableItemStateItemAlias.Finish >= to)         //right
                )
                .List();


                var trackService = new TrackService(this.Database);

                var trackItems = trackService.GetBetweenTimes(ids, from, to);

                //build map
                foreach (var item in items)
                {
                    var tmpDate = from;
                    while (tmpDate < to)
                    {

                        var mapItem = new AvailableCountMap()
                        {
                            Count = 0,
                            ItemId = item.Id,
                            Date = tmpDate
                        };
                        map.Add(mapItem);

                        switch (item.TrackingPeriod)
                        {
                            case Period.Minutely:
                                tmpDate = tmpDate.AddMinutes(item.TrackingStep);
                                break;
                            case Period.Hourly:
                                tmpDate.AddHours(item.TrackingStep);
                                break;
                            case Period.Daily:
                                tmpDate.AddDays(item.TrackingStep);
                                break;
                            case Period.Weekly:
                                tmpDate.AddDays(item.TrackingStep * 7);
                                break;
                            case Period.Monthly:
                                tmpDate.AddMonths(item.TrackingStep);
                                break;
                            default:
                                tmpDate.AddYears(item.TrackingStep);
                                break;
                        }

                    }
                }

                foreach (var item in items)
                {
                    var myStates = states.Where(state => state.Item.Id == item.Id).OrderBy(state => state.Start);
                    foreach (var state in myStates)
                    {
                        var qMap = map.Where(mapItem => mapItem.Date >= state.Start).ToList();
                        foreach (var mapItem in qMap)
                        {
                            mapItem.Count = state.Count;
                        }

                    }
                }

                foreach (var trackItem in trackItems)
                {
                    if (!trackItem.Blocked)
                    {
                        foreach (var mapItem in map.Where(mapItem => mapItem.ItemId == trackItem.Item.Id && mapItem.Date >= trackItem.Start))
                        {

                            mapItem.Count -= trackItem.Count;
                        }
                    }
                    else
                    {
                        foreach (var mapItem in map.Where(mapItem => mapItem.ItemId == trackItem.Item.Id && mapItem.Date >= trackItem.Start && mapItem.Date <= trackItem.Finish))
                        {

                            mapItem.Count -= trackItem.Count;
                        }
                    }
                }
                foreach (var mapItem in map)
                {
                    mapItem.Count -= count;

                }

                var result = map.Find(mapItem => mapItem.Count < 0);
                return result == null;
            }
        }

        public IEnumerable<TrackableItem> GetAllForProcessId(int processId)
        {
            using (var sess = this.Database.getSession())
            {
                TrackableItem trackableItemAlias = null;

                return sess.QueryOver<TrackableItem>(() => trackableItemAlias)
                .Where(() => trackableItemAlias.OwnerProcess.Id == processId)
                .List();
            }
        }

        public TrackableItem GetByTitleForProcess(string title, int processId)
        {
            using (var sess = this.Database.getSession())
            {
                TrackableItem trackableItemAlias = null;

                return sess.QueryOver<TrackableItem>(() => trackableItemAlias)
                .Where(() => trackableItemAlias.OwnerProcess.Id == processId)
                .And(() => trackableItemAlias.Title == title)
                .SingleOrDefault();
            }
        }
    }
}