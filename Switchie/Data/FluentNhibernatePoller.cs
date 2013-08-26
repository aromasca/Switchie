using System;
using System.Collections.Generic;
using NHibernate.Criterion;

namespace Switchie.Data
{
    public class FluentNhibernatePoller<T> : PollerBase<T> where T: class, IPollableItem //where T : IPollableItem
    {
        private readonly FeatureSessionFactory _FeatureSessionFactory;
        public FluentNhibernatePoller(TimeSpan interval)
            : base(interval)
        {
            _FeatureSessionFactory = new FeatureSessionFactory();
        }

        protected override IEnumerable<T> GetChanges(DateTime? lastCheck)
        {
            using (var session = _FeatureSessionFactory.GetSessionFactory().OpenSession())
            {
                var result = session.CreateCriteria<T>()
                                    .Add(Expression.Gt("LastModifiedDate", lastCheck ?? new DateTime(1960, 1, 1)))
                                    .List<T>();
                return result;
            }
        }
    }
}
