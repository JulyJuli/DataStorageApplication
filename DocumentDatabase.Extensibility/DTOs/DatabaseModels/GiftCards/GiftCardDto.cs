using DocumentDatabase.Extensibility.DatabaseModels;

namespace DocumentDatabase.Extensibility.DTOs.DatabaseModels.GiftCards
{
    public class GiftCardDto : ModelIdentifier
    {
        internal GiftCardDto() { }

        public GiftCardDto(string recepientName, string senderName, PriceType priceType)
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