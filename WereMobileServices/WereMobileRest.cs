using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WereMobileServices.Simple;

namespace WereMobileServices
{
    class WereMobileRest : IWereMobileRest
    {
        #region Add
        string IWereMobileRest.AddBarcode(string barcodenumber, string productid, string id)
        {
            using (var db = new WereMobileEntities())
            {
                Barcode barcode;

                barcode = db.Barcode.FirstOrDefault(b => b.ID.Equals(id));
                if (barcode == null)
                {
                    barcode = new Barcode();
                }

                barcode.ID = id;
                barcode.BarcodeNumber = barcodenumber;
                barcode.ProductID = productid;


                db.Barcode.AddOrUpdate(barcode);

                db.SaveChanges();

                return barcode.ID;
            }
        }

        string IWereMobileRest.AddContragent(Stream data)
        { 
            
            StreamReader reader = new StreamReader(data);
            string xmlString = reader.ReadToEnd();

            byte[] byteData = Convert.FromBase64String(xmlString);
            string decodedString = Encoding.UTF8.GetString(byteData);
            
            var contragents = JsonConvert.DeserializeObject<List<Contragent>>(decodedString);
            foreach (Contragent contragent in contragents)
            {
                using (var db = new WereMobileEntities())
                {
                    Contragent newContragent;

                    newContragent = db.Contragent.FirstOrDefault(p => p.ID.Equals(contragent.ID));

                    if (newContragent == null)
                    {
                        newContragent = new Contragent();
                    }

                    newContragent.ID = contragent.ID;
                    newContragent.Name = contragent.Name;
                    newContragent.Bulstat = contragent.Bulstat;
                    newContragent.VatNumber = contragent.VatNumber;
                    newContragent.City = contragent.City;
                    newContragent.Address = contragent.Address;
                    newContragent.Mrp = contragent.Mrp;
                    newContragent.PhoneNumber = contragent.PhoneNumber;


                    db.Contragent.AddOrUpdate(newContragent);

                    db.SaveChanges();

                }
            }

            return xmlString;

        }

        string IWereMobileRest.AddDocument(Stream data)
        {
            StreamReader reader = new StreamReader(data);
            string codedString = reader.ReadToEnd();

            byte[] byteData = Convert.FromBase64String(codedString);
            string decodedString = Encoding.UTF8.GetString(byteData);

            var fakedocuments = JsonConvert.DeserializeObject<List<FakeDocument>>(decodedString);

            foreach (var FakeDocument in fakedocuments)
            {
                using (var db = new WereMobileEntities())
                {
                    var newDocument = db.Document.FirstOrDefault(p => p.ID.Equals(FakeDocument.ID));
                    if (newDocument == null)
                    {
                        newDocument = new Document();
                    }
                    newDocument.ID = FakeDocument.ID;
                    newDocument.SourceID = FakeDocument.SourceID;
                    newDocument.DestinationID = FakeDocument.DestinationID;
                    newDocument.DocumentNumber = FakeDocument.DocumentNumber;

                    var dateFormated = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(FakeDocument.Date)).ToLocalTime();
                    var time = dateFormated.DateTime;

                    newDocument.Date = time;


                    db.Document.AddOrUpdate(newDocument);

                    db.SaveChanges();
                }
            }
            return codedString;
        }

        string IWereMobileRest.AddDocumentRow(Stream data)
        {
            StreamReader reader = new StreamReader(data);
            string xmlString = reader.ReadToEnd();

            byte[] byteData = Convert.FromBase64String(xmlString);
            string decodedString = Encoding.UTF8.GetString(byteData);

            var documentRows = JsonConvert.DeserializeObject<List<DocumentRow>>(decodedString);
            foreach (DocumentRow documentRow in documentRows)
            {
                using (var db = new WereMobileEntities())
                {
                    DocumentRow newDocumentRow;

                    newDocumentRow = db.DocumentRow.FirstOrDefault(dr => dr.ID.Equals(documentRow.ID));

                    if (newDocumentRow == null)
                    {
                        newDocumentRow = new DocumentRow();
                    }

                    newDocumentRow.ID = documentRow.ID;
                    newDocumentRow.ProductID = documentRow.ProductID;
                    newDocumentRow.LotID = documentRow.LotID;
                    newDocumentRow.Quantity = documentRow.Quantity;
                    newDocumentRow.Sum = documentRow.Sum;
                    newDocumentRow.DocumentID = documentRow.DocumentID;

                    db.DocumentRow.AddOrUpdate(newDocumentRow);

                    db.SaveChanges();
                }
            }
            return xmlString;
        }

        string IWereMobileRest.AddLot(Stream data)
        {
            StreamReader reader = new StreamReader(data);
            string xmlString = reader.ReadToEnd();

            byte[] byteData = Convert.FromBase64String(xmlString);
            string decodedString = Encoding.UTF8.GetString(byteData);
            //byte[] base64Decoded = DatatypeConverter.parseBase64Binary(contragentsBase64String);
            var fakelots = JsonConvert.DeserializeObject<List<FakeLot>>(decodedString);

            foreach (var FakeLot in fakelots)
            {
                using (var db = new WereMobileEntities())
                {
                    var newLot = db.Lot.FirstOrDefault(p => p.ID.Equals(FakeLot.ID));

                    if (newLot == null)
                    {
                        newLot = new Lot();
                    }

                    newLot.ID = FakeLot.ID;
                    newLot.ProductID = FakeLot.ProductID;
                    newLot.Quantity = FakeLot.Quantity;
                    var dateFormated = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(FakeLot.ExpirationDate)).ToLocalTime();
                    var time = dateFormated.DateTime;
                    newLot.ExpirationDate = time;
                    newLot.LotNumber = FakeLot.LotNumber;


                    db.Lot.AddOrUpdate(newLot);

                    db.SaveChanges();
                }
            }

            return xmlString;
        }

        string IWereMobileRest.AddProduct(string name, decimal price, double quantity, string number, string id)
        {
            using (var db = new WereMobileEntities())
            {
                Product prod;
                prod = db.Product.FirstOrDefault(p => p.ID.Equals(id));

                if (prod == null)
                {
                    prod = new Product();
                }

                prod.ID = id;
                prod.Name = name;
                prod.ProductNumber = number;
                prod.Price = price;
                prod.Quantity = quantity;


                db.Product.AddOrUpdate(prod);


                db.SaveChanges();

                return prod.ID;
            }
        }

        string IWereMobileRest.AddSetting(string name, string value, string description, string id)
        {
            using (var db = new WereMobileEntities())
            {
                Setting setting;

                setting = db.Setting.FirstOrDefault(s => s.ID.Equals(id));

                if (setting == null)
                {
                    setting = new Setting();
                }

                setting.ID = id;
                setting.Name = name;
                setting.Value = value;
                setting.Description = description;


                db.Setting.AddOrUpdate(setting);

                db.SaveChanges();

                return setting.ID;
            }
        }
        #endregion
        #region Delete
        bool IWereMobileRest.DeleteBarcode(string id)
        {
            using (var db = new WereMobileEntities())
            {
                var barcode = db.Barcode.FirstOrDefault(b => b.ID.Equals(id));

                if (barcode != null)
                {
                    db.Barcode.Remove(barcode);
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        bool IWereMobileRest.DeleteContragent(string id)
        {
            using (var db = new WereMobileEntities())
            {
                var contragent = db.Contragent.FirstOrDefault(p => p.ID.Equals(id));

                if (contragent != null)
                {
                    db.Contragent.Remove(contragent);
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        bool IWereMobileRest.DeleteDocument(string id)
        {
            using (var db = new WereMobileEntities())
            {
                var document = db.Document.FirstOrDefault(p => p.ID.Equals(id));

                if (document != null)
                {
                    db.Document.Remove(document);
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        bool IWereMobileRest.DeleteDocumentRow(string id)
        {
            using (var db = new WereMobileEntities())
            {
                var documentRow = db.DocumentRow.FirstOrDefault(dr => dr.ID.Equals(id));

                if (documentRow != null)
                {
                    db.DocumentRow.Remove(documentRow);
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        bool IWereMobileRest.DeleteLot(string id)
        {
            using (var db = new WereMobileEntities())
            {
                var lot = db.Lot.FirstOrDefault(l => l.ID.Equals(id));

                if (lot != null)
                {
                    db.Lot.Remove(lot);
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        bool IWereMobileRest.DeleteProduct(string id)
        {
            using (var db = new WereMobileEntities())
            {
                var product = db.Product.FirstOrDefault(p => p.ID.Equals(id));

                if (product != null)
                {
                    db.Product.Remove(product);
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        bool IWereMobileRest.DeleteSetting(string id)
        {
            using (var db = new WereMobileEntities())
            {
                var setting = db.Setting.FirstOrDefault(s => s.ID.Equals(id));

                if (setting != null)
                {
                    db.Setting.Remove(setting);
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        #endregion
        #region GetAll
        List<Simple.Barcode> IWereMobileRest.GetAllBarcodes()
        {
            using (var db = new WereMobileEntities())
            {
                var barcodes = db.Barcode.Select(b => new Simple.Barcode
                {
                    ID = b.ID,
                    BarcodeNumber = b.BarcodeNumber,
                    ProductID = b.ProductID
                }).ToList();

                return barcodes;
            }
        }

        List<Simple.Contragent> IWereMobileRest.GetAllContragents()
        {
            using (var db = new WereMobileEntities())
            {
                var contragents = db.Contragent.Select(p => new Simple.Contragent
                {
                    ID = p.ID,
                    Name = p.Name,
                    Bulstat = p.Bulstat,
                    VatNumber = p.VatNumber,
                    City = p.City,
                    Address = p.Address,
                    Mrp = p.Mrp,
                    PhoneNumber = p.PhoneNumber,
                }).ToList();

                return contragents;
            }
        }

        List<Simple.DocumentRow> IWereMobileRest.GetAllDocumentRows()
        {
            using (var db = new WereMobileEntities())
            {
                var documentRows = db.DocumentRow.Select(dr => new Simple.DocumentRow
                {
                    ID = dr.ID,
                    ProductID = dr.ProductID,
                    LotID = dr.LotID,
                    Quantity = dr.Quantity,
                    Sum = dr.Sum,
                    DocumentID = dr.DocumentID
                }).ToList();

                return documentRows;
            }
        }

        List<Simple.Document> IWereMobileRest.GetAllDocuments()
        {
            using (var db = new WereMobileEntities())
            {
                var documents = db.Document.Select(p => new Simple.Document
                {
                    ID = p.ID,
                    SourceID = p.SourceID,
                    DestinationID = p.DestinationID,
                    DocumentNumber = p.DocumentNumber,
                    Date = p.Date
                }).ToList();

                return documents;
            }
        }

        List<Simple.Lot> IWereMobileRest.GetAllLots()
        {
            using (var db = new WereMobileEntities())
            {
                var lots = db.Lot.Select(l => new Simple.Lot
                {
                    ID = l.ID,
                    ProductID = l.ProductID,
                    Quantity = l.Quantity,
                    ExpirationDate = l.ExpirationDate,
                    LotNumber = l.LotNumber
                }).ToList();

                return lots;
            }
        }

        List<Simple.Product> IWereMobileRest.GetAllProducts()
        {
            using (var db = new WereMobileEntities())
            {
                var products = db.Product.Select(p => new Simple.Product
                {
                    ID = p.ID,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    ProductNumber = p.ProductNumber
                }).ToList();

                return products;
            }
        }

        List<Setting> IWereMobileRest.GetAllSettings()
        {
            using (var db = new WereMobileEntities())
            {
                var settings = db.Setting.ToList();

                return settings;
            }
        }
        #endregion
    }
}
