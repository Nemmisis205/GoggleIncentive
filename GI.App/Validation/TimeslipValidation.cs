using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using GI.Repository.UOW;
using GI.App.Validation;

namespace GI.App.Validation
{
    public class TimeslipValidation : IDataErrorInfo
    {
        private UnitOfWork _UOW { get; set; }
        public string Operator { get; set; }
        public string StartCounter { get; set; }
        public string EndCounter { get; set; }
        public string Goggle { get; set; }
        public string StartBox { get; set; }
        public string StartPiece { get; set; }
        public string EndBox { get; set; }
        public string EndPiece { get; set; }

        public string this[string ColumnName]
        {
            get
            {
                string? message = null;
                int result;
                if (ColumnName == "StartCounter")
                {
                    if (!int.TryParse(StartCounter, out result))
                    {
                        message = "Start Counter must be a whole number.";
                    }
                }
                if (ColumnName == "EndCounter")
                {
                    if (!int.TryParse(EndCounter, out result))
                    {
                        message = "End Counter must be a whole number.";
                    }
                }
                if (ColumnName == "StartBox")
                {
                    if (!int.TryParse(StartBox, out result))
                    {
                        message = "Beginning Box Count must be a whole number.";
                    }
                }
                if (ColumnName == "StartPiece")
                {
                    if (!int.TryParse(StartPiece, out result))
                    {
                        message = "Beginning Piece Count must be a whole number.";
                    }
                }
                if (ColumnName == "EndBox")
                {
                    if (!int.TryParse(EndBox, out result))
                    {
                        message = "End Box Count must be a whole number.";
                    }
                }
                if (ColumnName == "EndPiece")
                {
                    if (!int.TryParse(EndPiece, out result))
                    {
                        message = "End Piece Count must be a whole number.";
                    }
                }
                if (ColumnName == "Operator")
                {
                    if (_UOW.Operators.GetByName(Operator) == null)
                    {
                        message = "Operator name invalid. Select from pull down, or add operator first.";
                    }
                }
                if (ColumnName == "Goggle")
                {
                    if (_UOW.Goggles.GetByName(Goggle) == null)
                    {
                        message = "Goggle name invalid. Select from pull down, or add goggle first.";
                    }
                }


                return message;
            }
        }
        public TimeslipValidation(UnitOfWork UOW) { _UOW = UOW; }
        public string Error
        {
            get
            {
                if (!string.IsNullOrEmpty(this["Goggle"]))
                {
                    return this["Goggle"];
                }
                else if (!string.IsNullOrEmpty(this["Operator"]))
                {
                    return this["Operator"];
                }
                else if (!string.IsNullOrEmpty(this["StartCounter"]))
                {
                    return this["StartCounter"];
                }
                else if (!string.IsNullOrEmpty(this["EndCounter"]))
                {
                    return this["EndCounter"];
                }
                else if (!string.IsNullOrEmpty(this["StartBox"]))
                {
                    return this["StartBox"];
                }
                else if (!string.IsNullOrEmpty(this["StartPiece"]))
                {
                    return this["StartPiece"];
                }
                else if (!string.IsNullOrEmpty(this["EndBox"]))
                {
                    return this["EndBox"];
                }
                else if (!string.IsNullOrEmpty(this["EndPiece"]))
                {
                    return this["EndPiece"];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
