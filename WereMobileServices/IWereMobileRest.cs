using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WereMobileServices
{
    [ServiceContract]
    interface IWereMobileRest
    {
        #region Product
        //product
        //get all products form database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"GetAllProducts")]
        List<Simple.Product> GetAllProducts();

        //delete product from database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"DeleteProduct?id={id}")]
        bool DeleteProduct(string id);

        //add product to database
        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = @"AddProduct?name={name}&price={price}&quantity={quantity}&number={number}&id={id}")]
        string AddProduct(string name, decimal price, double quantity, string number, string id);
        #endregion
        #region Lot
        //lot
        //get all lots form database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"GetAllLots")]
        List<Simple.Lot> GetAllLots();

        //delete lot from database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"DeleteLot?id={id}")]
        bool DeleteLot(string id);

        //add lot to database
        [OperationContract]
        [WebInvoke(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"AddLot")]
        string AddLot(Stream data);
        #endregion
        #region Barcode
        //barcodes
        //get all barcodes form database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"GetAllBarcodes")]
        List<Simple.Barcode> GetAllBarcodes();

        //delete barcode from database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"DeleteBarcode?id={id}")]
        bool DeleteBarcode(string id);

        //add barcode from database
        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = @"AddBarcode?barcodenumber={barcodenumber}&productid={productid}&id={id}")]
        string AddBarcode(string barcodenumber, string productid, string id);
        #endregion
        #region Setting
        //settings
        //get all settings form database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"GetAllSettings")]
        List<Setting> GetAllSettings();

        //delete setting from database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"DeleteSetting?id={id}")]
        bool DeleteSetting(string id);

        //add setting from database
        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = @"AddSetting?name={name}&value={value}&description={description}&id={id}")]
        string AddSetting(string name, string value, string description, string id);
        #endregion
        #region Document Row
        //documentRows
        //get all documentRows form database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"GetAllDocumentRows")]
        List<Simple.DocumentRow> GetAllDocumentRows();

        //delete documentRow from database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"DeleteDocumentRow?id={id}")]
        bool DeleteDocumentRow(string id);

        //add documentRow from database
        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = @"AddDocumentRow")]
        string AddDocumentRow(Stream data);
        #endregion
        #region Contragent
        //contragent
        //get all contragents form database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"GetAllContragents")]
        List<Simple.Contragent> GetAllContragents();

        //delete contragent from database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"DeleteContragent?id={id}")]
        bool DeleteContragent(string id);

        //add contragent from database
        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = @"AddContragent")]
        string AddContragent(Stream data);
        #endregion
        #region Document
        //document
        //get all documneets form database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"GetAllDocuments")]
        List<Simple.Document> GetAllDocuments();

        //delete document from database
        [OperationContract]
        [WebGet(
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            UriTemplate = @"DeleteDocument?id={id}")]
        bool DeleteDocument(string id);

        //add document from database
        [OperationContract]
        [WebInvoke(
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = @"AddDocument")]
        string AddDocument(Stream data);
        #endregion
    }
}
