using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EntityModels
{
    public class TransactionModel
    {
        public int Id { get; set; }

        public int WalletId { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Value is required")]
        [RegularExpression(@"^\d{1,16}(\,\d{1,2})?$", ErrorMessage = "Value is Invalid")]
        [DecimalValidatior(ErrorMessage = "Value is Invalid")]
        public string Value { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date, ErrorMessage="Date is Invalid")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date")]
        public DateTime? Date { get; set; }


        [Required(ErrorMessage = "Hour is required")]
        [DataType(DataType.Time, ErrorMessage = "Hour is Invalid")]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("Hour")]
        [TimeSpamValidator(ErrorMessage = "Correct format is ##:##")]
        public TimeSpan? Time { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [DisplayName("Movimentation")]
        public TransactionType Type { get; set; }

        public bool IsEditing { get; set; }

        public bool IsColoredRow { get; set; }

        public string Error { get; set; }

        public decimal valueToTransact { get; set; }
        public bool ToDebit { get; set; }
    }
}