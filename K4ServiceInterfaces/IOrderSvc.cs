using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaiTrade.Interfaces;

namespace K4ServiceInterface
{
    public interface IOrderSvc
    {
        void AssociateClOrdID(string myClOrdID, KaiTrade.Interfaces.IOrder myOrder);
        KaiTrade.Interfaces.IOrder CreateOrder();
        void DeleteOrder(string myID);
        KaiTrade.Interfaces.IOrder GetOrder(string myOrderID);
        System.Collections.Generic.List<string> GetOrderIDS();
        System.Collections.Generic.List<string> GetOrderIDS(string user);
        KaiTrade.Interfaces.IOrder GetOrderWithClOrdIDID(string myID);
        string InvertSide(string mySide);
        void LoadOrdersFromFile(string myFilePath);
        
        bool RecordExecutionReport(string myID, KaiTrade.Interfaces.IFill fill);
        void RegisterOrder(KaiTrade.Interfaces.IOrder myOrder);
        void SetChanged(string myID);
        void StoreOrdersToFile(string myFilePath);
        void SubmitOrder(KaiTrade.Interfaces.IOrder order);
        void ToFileJSON(string myOrderFilePath, string myFillsFilePath);
        IVenueSvc VenueSvc { get; set; }
    }
}
