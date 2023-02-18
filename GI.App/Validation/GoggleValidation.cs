using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GI.Repository.UOW;

namespace GI.App.Validation
{
    public class GoggleValidation : IDataErrorInfo
    {
        private UnitOfWork _UOW;
        public string Quote;
        public string PerBox;
        public string BoxesPerPallet;

        public GoggleValidation(UnitOfWork UOW)
        {
            _UOW = UOW;
        }

        public string this[string ColumnName]
        {
            get
            {
                string? message = null;
                int result;
                if (ColumnName == "Quote")
                {
                    if (!int.TryParse(Quote, out result))
                    {
                        message = "Quoted Cycles Per Hour must be a whole number.";
                    }
                }
                if (ColumnName == "PerBox")
                {
                    if (!int.TryParse(PerBox, out result))
                    {
                        message = "Goggle Per Box must be a whole number.";
                    }
                }
                if (ColumnName == "BoxesPerPallet")
                {
                    if (!int.TryParse(BoxesPerPallet, out result))
                    {
                        message = "Boxes Per Pallet must be a whole number.";
                    }
                }
                return message;
            }
        }

        public string Error
        {
            get
            {
                if (!string.IsNullOrEmpty(this["Quote"]))
                {
                    return this["Quote"];
                }
                else if (!string.IsNullOrEmpty(this["PerBox"]))
                {
                    return this["PerBox"];
                }
                else if (!string.IsNullOrEmpty(this["BoxesPerPallet"]))
                {
                    return this["BoxesPerPallet"];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
