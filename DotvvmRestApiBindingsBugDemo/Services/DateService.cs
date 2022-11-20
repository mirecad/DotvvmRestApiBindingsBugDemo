using System;
using DotVVM.Framework.ViewModel;

namespace DotvvmRestApiBindingsBugDemo.Services;

public class DateService
{
    [AllowStaticCommand]
    public DateTime GetTime()
    {
        return DateTime.Now;
    }
}