using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using TimeTracker.Core.Data;
using TimeTracker.Core.Models;

namespace TimeTracker.Core.Services.Implements
{
    public class TrackService : ABaseService<Track>, ITrackService
    {
        public TrackService(INHibernateHelper database) : base(database)
        { }

        public Track CreateTrack(int itemId, DateTime from, DateTime to, int count = 1)
        {
            using (var sess = this.Database.getSession())
            {
                var track = new Track()
                {
                    Blocked = true,
                    Start = from,
                    Finish = to,
                    Count = count,
                    Item = sess.QueryOver<TrackableItem>().Where(item => item.Id == itemId).SingleOrDefault()

                };
                sess.Save(track);
                return track;
            }

        }

        public Track FinishTrack(int trackId, DateTime finishDate)
        {
            using (var sess = this.Database.getSession())
            {
                var track = sess.QueryOver<Track>().Where(item => item.Id == trackId).SingleOrDefault();
                track.Blocked = true;
                track.Finish = finishDate;

                sess.Save(track);
                return track;
            }
        }
        public Track StartTrack(int itemId, DateTime from)
        {
            using (var sess = this.Database.getSession())
            {
                var track = new Track()
                {
                    Blocked = false,
                    Start = from,
                    Item = sess.QueryOver<TrackableItem>().Where(item => item.Id == itemId).SingleOrDefault()

                };
                sess.Save(track);
                return track;
            }
        }
        public IEnumerable<Track> GetBetweenTimes(int id, DateTime from, DateTime to)
        {
            return this.GetBetweenTimes(new int[] { id }, from, to);
        }
        public IEnumerable<Track> GetBetweenTimes(int[] ids, DateTime from, DateTime to)
        {
            using (var sess = this.Database.getSession())
            {
                return this.GetBetweenTimes(ids, from, to, sess);
            }
        }

        public IEnumerable<Track> ReleaseTrackbleItemBetweenTimes(int itemId, DateTime from, DateTime to)
        {

            using (var sess = this.Database.getSession())
            {
                var tracks = this.GetBetweenTimes(itemId, from, to, sess);
                using (var trans = sess.BeginTransaction())
                {

                    foreach (var track in tracks)
                    {
                        track.Canceled = true;
                        sess.SaveOrUpdate(track);
                    }
                    trans.Commit();
                }

                return tracks;
            }
        }

        public IEnumerable<Track> ChangeTrackableCountBetweenDates(int itemId, int count, DateTime from, DateTime to)
        {
            return this.ChangeTrackableCountBetweenDates(new int[] { itemId }, count, from, to);
        }

        public IEnumerable<Track> ChangeTrackableCountBetweenDates(int[] itemIds, int count, DateTime from, DateTime to)
        {
            using (var sess = this.Database.getSession())
            {
                var tracks = this.GetBetweenTimes(itemIds, from, to);

                foreach (var track in tracks)
                {
                    track.Count = count;
                }

                return this.Save(tracks, sess);
            }
        }

        private IEnumerable<Track> Save(IEnumerable<Track> list, ISession sess)
        {

            using (var trans = sess.BeginTransaction())
            {

                foreach (var track in list)
                {
                    sess.SaveOrUpdate(track);
                }
                trans.Commit();
            }
            return list;
        }

        private IEnumerable<Track> GetBetweenTimes(int id, DateTime from, DateTime to, ISession sess)
        {
            return this.GetBetweenTimes(new int[] { id }, from, to, sess);
        }
        private IEnumerable<Track> GetBetweenTimes(int[] ids, DateTime from, DateTime to, ISession sess)
        {
            Track trackAlias = null;
            TrackableItem trackableItemAlias = null;

            return sess.QueryOver<Track>(() => trackAlias)
            .Left.JoinAlias(() => trackAlias.Item, () => trackableItemAlias)
            .WhereRestrictionOn(() => trackAlias.Item.Id).IsIn(ids)
            .And(() => trackAlias.Canceled != true)
            .And(Restrictions.Disjunction()
                .Add(() => trackAlias.Start >= from && trackAlias.Finish < to)       //inner
                .Add(() => trackAlias.Start <= from && trackAlias.Finish >= from)    //outer
                .Add(() => trackAlias.Start <= from && trackAlias.Finish > from)     //left
                .Add(() => trackAlias.Start < to && trackAlias.Finish >= to)         //right
            ).List();
        }
    }
}