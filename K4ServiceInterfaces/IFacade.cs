using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KaiTrade.Interfaces;

namespace K4ServiceInterface
{
    public interface IFacade
    {
        /*
        IProductManager ProductManager
        { get; set; }
       // KaiTrade.Interfaces.IPublisher CreatePxPub(KaiTrade.Interfaces.IProduct product);
        void AddProduct(string genericName, string tradeVenue);
        KaiTrade.Interfaces.IProduct AddProduct(string mnemonic, string Name, string mySecID, string myExchangeID, string ExDestination, string myCFICode, string myMMY, string myCurrency, double? strikePx, bool doEvent);
        string AppPath { get; set; }
        //IFactory Factory { get; }
        void RequestProductDetails(KaiTrade.Interfaces.IProduct prod);
        //IL1PX GetL1Prices(KaiTrade.Interfaces.IProduct product);
        //void SetL1Prices(KaiTrade.Interfaces.IProduct product, IL1PX L1Price);

        //KaiTrade.Interfaces.IDOM  GetDOM(KaiTrade.Interfaces.IProduct product);
        //void SetDOM(KaiTrade.Interfaces.IProduct product, KaiTrade.Interfaces.IDOM DOM);
        IPriceHandler PriceHandler { get; set; }

        string GetUserProfileProperty(string section, string propertyName);
        //void RaiseAlert("KTACQG", "CQG Data Connection is Down", 0, KaiTrade.Interfaces.ErrorLevel.fatal, KaiTrade.Interfaces.FlashMessageType.error);
        void ProcessPositionUpdate(KaiTrade.Interfaces.IPosition position);

        ITradeSignalManager TradeSignalManager
        { get; set; }

        void ProcessITradeSignal(ITSSet mySet, ITradeSignal signal);

        void UpdateOrderInformation(IOrder order, List<IFill> fills);

        IOrderSvc OrderService
        { get; set; }

        void ApplyUpdate(KaiTrade.Interfaces.IPXUpdate update);
        IPriceAgregator GetPriceAgregator(string name);

        //KaiTrade.Interfaces.IPublisher GetPXPublisher(string mnmonic);
        //object K2ServiceClient { get; set; }
         */
    }
}
