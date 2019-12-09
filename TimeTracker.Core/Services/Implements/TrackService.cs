using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using TimeTracker.Core.Data;
using TimeTracker.Core.Models;

namespace TimeTracker.Core.Services.Implements
{
    public class TrackService : ABaseService<Track>, ITrackService
    {
        public TrackService(INHibernateHelper database) : base(database)
        {
        }

        public bool CheckAvailableCountBetweenTime(int id, int count, DateTime from, DateTime to)
        {
            using (var sess = this.Database.getSession())
            {
                TrackableItem trackableItemAlias = null;
                Track trackAlias = null;

                int totalCount = sess.QueryOver<TrackableItem>(() => trackableItemAlias)
                .Where(() => trackableItemAlias.Id == id).SingleOrDefault().Count;

                sess.QueryOver<Track>(() => trackAlias)
                .Left.JoinAlias(() => trackAlias.Item, () => trackableItemAlias)
                .Where(() => trackAlias.Item.Id == id)
                .And(Restrictions.Disjunction()
                    .Add(() => trackAlias.Start >= from && trackAlias.End < to)       //inner
                    .Add(() => trackAlias.Start <= from && trackAlias.End >= from)    //outer
                    .Add(() => trackAlias.Start <= from && trackAlias.End > from)     //left
                    .Add(() => trackAlias.Start < to && trackAlias.End >= to)         //right
                ).List();

                return false;
            }
        }

        public IEnumerable<Track> GetBetweenTimes(int id, DateTime from, DateTime to)
        {
            using (var sess = this.Database.getSession())
            {

                Track trackAlias = null;
                TrackableItem trackableItemAlias = null;

                return sess.QueryOver<Track>(() => trackAlias)
                .Left.JoinAlias(() => trackAlias.Item, () => trackableItemAlias)
                .Where(() => trackAlias.Item.Id == id)
                .And(Restrictions.Disjunction()
                    .Add(() => trackAlias.Start >= from && trackAlias.End < to)       //inner
                    .Add(() => trackAlias.Start <= from && trackAlias.End >= from)    //outer
                    .Add(() => trackAlias.Start <= from && trackAlias.End > from)     //left
                    .Add(() => trackAlias.Start < to && trackAlias.End >= to)         //right
                ).List();
            }
        }
    }
}