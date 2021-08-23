using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BA_App.Enum
{
    public enum EnumPicKerFilter
    {
        [Description("Tất cả")]
        TatCa = 0,
        [Description("Mobile")]
        Mobile = 1,
        [Description("QA")]
        QA = 2,
        [Description("Phần mềm")]
        PhanMem = 3,
    }
}
