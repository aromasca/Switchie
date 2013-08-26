using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Switchie.Data
{
    public class ItemChangedArgs<T> : EventArgs
    {
        public T Key { get; set; }
    }

    public class ItemsChangedArgs<T> : EventArgs
    {
        public IEnumerable<T> ItemsChanged { get; set; }
    }

    public abstract class PollerBase<T> where T: IPollableItem
    {
        public event EventHandler<ItemsChangedArgs<T>> ItemsChanged;
        readonly Timer timer;
        public TimeSpan Interval { get; set; }
        private volatile bool running;
        protected DateTime? LastTime { get; set; }

        protected virtual void OnItemsChanged(ItemsChangedArgs<T> e)
        {
            var evt = ItemsChanged;
            if (evt != null)
                evt(this, e);
        }

        protected abstract IEnumerable<T> GetChanges(DateTime? lastCheck);

        public PollerBase(TimeSpan interval)
        {
            Interval = interval;
            timer = new Timer(Interval.TotalMilliseconds);
            timer.Elapsed += timer_Elapsed;
        }

        public void Start()
        {
            
            running = true;
            if (LastTime == null)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(o=> Poll());
            }
            timer.Start();
        }
        public void Stop()
        {
            running = false;
            timer.Stop();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            timer.Interval = Interval.TotalMilliseconds;
            Poll();

            if (running)
                timer.Start();
        }

        protected void Poll()
        {
            var items = GetChanges(LastTime).ToArray();
            LastTime = items.Length > 0 ? items.Max(s => s.LastModifiedDate) : LastTime;
            if (items.Any())
                OnItemsChanged(new ItemsChangedArgs<T> { ItemsChanged = items});
        }
    }

    public interface IPollableItem
    {
        DateTime LastModifiedDate { get; set; }
    }
}
