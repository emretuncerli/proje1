using MongoDB.Bson;               //MongoDB için dosya formatı (MongoDB veritipleri bu isim uzayı altında bulunuyor.)
using MongoDB.Driver;             //MongoDB bağlantısı için gerekli kütüphane
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;     //Asenkron meotlarımızı bu isim uzayı sayesinde çağırabiliyoruz.

class Program
{
    static void Main(string[] args)
    {
        //----------KAYIT EKLEME METODU--------
        /* Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < 100000; i++)
          {
          MainAsync().Wait();    
          }
         sw.Stop();
          Console.WriteLine("100000 kayıt için geçen Ekleme süresi " + sw.ElapsedMilliseconds.ToString() + " milisaniyedir.");*/

        //----------KAYIT GÜNCELLEME METODU--------
         /*Stopwatch sw = Stopwatch.StartNew();
         for (int i = 0; i < 100000; i++)
         {
             update(args).Wait();
         }
         sw.Stop();
         Console.WriteLine("100000 kayıt için geçen güncelleme süresi " + sw.ElapsedMilliseconds.ToString() + " milisaniyedir.");*/


        //----------KAYIT SİLME METODU--------
        /*Stopwatch sw = Stopwatch.StartNew();
         for (int i = 0; i < 100000; i++)
         {
             silme(args).Wait();
         }
            sw.Stop();
             Console.WriteLine("100000 kayıt için geçen Silme süresi " + sw.ElapsedMilliseconds.ToString() + " milisaniyedir.");*/


        //----------KAYIT LİSTELEME METODU--------
        /*Stopwatch sw = Stopwatch.StartNew();
        limit(args).Wait();
        sw.Stop();
        Console.WriteLine("100000 kayıt için geçen Listeleme süresi " + sw.ElapsedMilliseconds.ToString() + " milisaniyedir.");*/

        //sıralama(args).Wait();
    }

    ////----------KAYIT EKLEME METODU--------
    static async Task MainAsync()
    {
        var client = new MongoClient(); //Veritabanına erişim(Varsayılan olarak, 27017 bağlantı noktasındaki
                                        //bir örneğe bağlanan parametresiz bir denetleyici)
        IMongoDatabase db = client.GetDatabase("okul"); //"okul" isminde bir veritabanı oluşturuldu.
        var collection = db.GetCollection<BsonDocument>("ogrenciler"); //Kollection oluşturuldu.(Veritabanındaki tablo)
        var yeniOgrenci = yeniOgrenciOlustur();
        await collection.InsertManyAsync(yeniOgrenci);
        //   var belge = new BsonDocument   //BsonDocument, BSON değerine dize bir sözlüktür, bu yüzden herhangi bir sözlükte olduğu gibi başlatabiliriz
    }   
        //IEnumerable interface’i ise bir sınıfa foreach mekanizması tarafından tanınması için gerekli yetenekleri/nitelikleri kazandırır.
        private static IEnumerable<BsonDocument> yeniOgrenciOlustur()
        {

            var ogrenci1 = new BsonDocument
    {
      {"isim", "Emre"},
      {"soyad", "Tuncerli"},
      {"dersler", "Fizik"},
      {"sınıf", "3"},
      {"yaş", 23}
    };

           /* var ogrenci2 = new BsonDocument
    {
      {"isim", "Ahmet"},
      {"soyad", "Bilen"},
      {"dersler", new BsonArray {"İşletim Sistemleri", "Veri yapıları", "Algoritmalar"}},
      {"sınıf", "3"},
      {"yaş", 25}
    };

            var ogrenci3 = new BsonDocument
    {
      {"isim", "İsmail"},
      {"soyad", "Yağmur"},
      {"dersler", new BsonArray {"Tarih", "Otomata", "İspanyolca", "Veri iletişimi"}},
      {"sınıf", "2"},
      {"yaş", 26}
    };*/

     var yeniOgrenciler = new List<BsonDocument>();
     yeniOgrenciler.Add(ogrenci1);
        //yeniOgrenciler.Add(ogrenci2);
        //yeniOgrenciler.Add(ogrenci3);

        return yeniOgrenciler;
        }

    //----------KAYIT GÜNCELLEME METODU--------
    static async Task update(string[] args)
    {
        var conString = "mongodb://localhost:27017";
        var Client = new MongoClient(conString);
        var DB = Client.GetDatabase("okul");
        var collection = DB.GetCollection<BsonDocument>("ogrenciler");

        //isim Emre olanı , Emre123 ile değiştirir.
        var result = await collection.FindOneAndUpdateAsync(
                            Builders<BsonDocument>.Filter.Eq("isim", "Emre"),
                            Builders<BsonDocument>.Update.Set("isim", "Emree")
                            );

        //Verileri koleksiyondan geri alır
        //await collection.Find(new BsonDocument())
        // .ForEachAsync(x => Console.WriteLine(x));
    }


    //----------KAYIT SİLME METODU--------
    static async Task silme(string[] args)
    {
        var conString = "mongodb://localhost:27017";
        var Client = new MongoClient(conString);
        var DB = Client.GetDatabase("okul");
        var collection = DB.GetCollection<BsonDocument>("ogrenciler");
        var Deleteone = await collection.DeleteOneAsync(
                        Builders<BsonDocument>.Filter.Eq("isim", "Emree"));
    }


        static async Task limit(string[] args)
    {
        var conString = "mongodb://localhost:27017";
        var Client = new MongoClient(conString);
        var DB = Client.GetDatabase("okul");
        var collection = DB.GetCollection<BsonDocument>("ogrenciler");
        var list = await collection.Find(new BsonDocument())
                    .Limit(100000) 
                    .ToListAsync();
        foreach (var docs in list)
        {
            Console.WriteLine(docs);
        }
    }





    static async Task sıralama(string[] args)
    {
        var conString = "mongodb://localhost:27017";
        var Client = new MongoClient(conString);
        var DB = Client.GetDatabase("okul");
        var collection = DB.GetCollection<BsonDocument>("ogrenciler");
        var list = await collection.Find(new BsonDocument())
                   .Sort(Builders<BsonDocument>.Sort.Ascending("sınıf"))
                   .ToListAsync();
        foreach (var doc in list)
        {
            Console.WriteLine(doc);
        }
    }

} 