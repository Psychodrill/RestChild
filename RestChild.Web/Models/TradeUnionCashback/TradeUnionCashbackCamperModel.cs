using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using DocumentFormat.OpenXml.Math;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.Web.Models.TradeUnion;

namespace RestChild.Web.Models.TradeUnionCashback
{
    [DataContract]
    [Serializable]
    public class TradeUnionCashbackCamperModel : TradeUnionCamperModel
    {
        public TradeUnionCashbackCamperModel() : base()
        {
        }

        public TradeUnionCashbackCamperModel(TradeUnionCamper entity) : base(entity)
        {
            CashbackEstimatedAmount = entity.CashbackEstimatedAmount;
            CashbackBaseEstimatedAmount = entity.CashbackBaseEstimatedAmount;
            ContractDate = entity.ContractDate.FormatEx(string.Empty, string.Empty);;
            ContractNumber = entity.ContractNumber;
            FactDateIn = entity.FactDateIn.FormatEx(string.Empty, string.Empty);;
            FactDateOut = entity.FactDateOut.FormatEx(string.Empty, string.Empty);;
            CashbackRequested = entity.CashbackRequested ?? false;
            PrivilegePartId = entity.PrivilegePartId;
        }

        /// <summary>
        ///     Расчетная сумма кэшбека
        /// </summary>
        [Display(Description = "Расчетная сумма кэшбека")]
        [DataMember(Name = "CashbackEstimatedAmount")]
        public decimal? CashbackEstimatedAmount { get; set; }

        /// <summary>
        ///     База для расчета суммы кэшбека
        /// </summary>
        [Display(Description = "База для расчета суммы кэшбека")]
        [DataMember(Name = "CashbackBaseEstimatedAmount")]
        public decimal? CashbackBaseEstimatedAmount { get; set; }

        /// <summary>
        ///     Дата заключения договора
        /// </summary>
        [Display(Description = "Дата заключения договора")]
        [DataMember(Name = "ContractDate")]
        public string ContractDate { get; set; }

        /// <summary>
        ///     Номер договора
        /// </summary>
        [Display(Description = "Номер договора")]
        [MaxLength(1000, ErrorMessage = "\"Номер договора\" не может быть больше 1000 символов")]
        [DataMember(Name = "ContractNumber")]
        public string ContractNumber { get; set; }

        /// <summary>
        ///     Фактическая дата заезда
        /// </summary>
        [Display(Description = "Фактическая дата заезда")]
        [DataMember(Name = "FactDateIn")]
        public string FactDateIn { get; set; }

        /// <summary>
        ///     Фактическая дата выезда
        /// </summary>
        [Display(Description = "Фактическая дата выезда")]
        [DataMember(Name = "FactDateOut")]
        public string FactDateOut { get; set; }

        /// <summary>
        ///     Запрашивался кэшбэк
        /// </summary>
        [Display(Description = "Запрашивался кэшбэк")]
        [DataMember(Name = "CashbackRequested")]
        public bool CashbackRequested { get; set; }

        /// <summary>
        ///     Признак льготы
        /// </summary>
        [DataMember(Name = "PrivilegePartId")]
        [Display(Description = "Признак льготы")]
        public long? PrivilegePartId { get; set; }

        public override TradeUnionCamper BuildEntity()
        {
            var tuc = base.BuildEntity();

            tuc.CashbackEstimatedAmount = CashbackEstimatedAmount;
            tuc.CashbackBaseEstimatedAmount = CashbackBaseEstimatedAmount;
            tuc.ContractDate = ContractDate.TryParseDateDdMmYyyy();
            tuc.ContractNumber = ContractNumber;
            tuc.FactDateIn = FactDateIn.TryParseDateDdMmYyyy();
            tuc.FactDateOut = FactDateOut.TryParseDateDdMmYyyy();
            tuc.CashbackRequested = CashbackRequested;
            tuc.PrivilegePartId = PrivilegePartId;

            return tuc;
        }
    }
}
