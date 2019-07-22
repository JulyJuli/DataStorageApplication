using DocumentDatabase.Extensibility.DTOs;

namespace DataStorageApplication.TestProject.DatabaseModels.GiftCards
{
    public class GiftCardDto : ModelIdentifier
    {
        internal GiftCardDto() { }

        public GiftCardDto(string recepientName, string senderName, PriceType priceType, string id)
            : base(id)
        {
            RecepientName = recepientName;
            SenderName = senderName;
            PriceType = priceType;
        }

        public string RecepientName { get; set; }

        public string SenderName { get; set; }

        public PriceType PriceType { get; set; }
    }
}