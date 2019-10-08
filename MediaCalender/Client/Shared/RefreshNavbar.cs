using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCalender.Client.Shared
{
    public interface IRefreshNavbar
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }

    public class RefreshNavbar : IRefreshNavbar
    {
        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
