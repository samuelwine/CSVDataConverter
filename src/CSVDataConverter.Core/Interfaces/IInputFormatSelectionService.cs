﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDataConverter.Core.Interfaces
{
    public interface IInputFormatSelectionService
    {
        IDataFormatConverter GetSelectedInputFormat();
    }
}
