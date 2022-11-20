using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotvvmRestApiBindingsBugDemo.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {

		public DateTime ActualTimeFromViewModel => DateTime.Now;
		public DateTime ActualTimeFromRestApi { get; set;}
		public DateTime ActualTimeFromStaticCommandServices { get; set;}
		public DateTime ActualTimeViewModelStaticCommand { get; set;}

        [AllowStaticCommand]
        public static DateTime GetTime() => DateTime.Now;
    }
}
