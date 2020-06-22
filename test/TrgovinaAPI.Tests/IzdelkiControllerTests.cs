using System;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrgovinaAPI.Controllers;
using TrgovinaAPI.Models;

namespace TrgovinaAPI.Tests
{
    public class IzdelkiControllerTests : IDisposable
    {
        DbContextOptionsBuilder<IzdelekContext> optionsBuilder;
        IzdelekContext dbContext;
        IzdelkiController controller;

        public IzdelkiControllerTests()
        {
        optionsBuilder = new DbContextOptionsBuilder<IzdelekContext>();
        optionsBuilder.UseInMemoryDatabase("UnitTestInMemDB");
        dbContext = new IzdelekContext(optionsBuilder.Options);

        controller = new IzdelkiController(dbContext);
        }

        public void Dispose()
        {
        optionsBuilder = null;
        foreach (var izdelek in dbContext.Izdelki)
            {
                dbContext.Izdelki.Remove(izdelek);
            }
        dbContext.SaveChanges();
        dbContext.Dispose();
        controller = null;
        }

        [Fact]
        public void GetIzdelki_Vrne0Izdelkov_CeJeDBPrazna()
        {
        //Given
        
        //When
        var result = controller.GetIzdelki();
        //Then
        Assert.Empty(result.Value);
        }

        [Fact]
        public void GetIzdelkiVrne1IzdelekCeJeVDB1Objekt()
        {
        //Given
        var izdelek = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };

        dbContext.Izdelki.Add(izdelek);
        dbContext.SaveChanges();

        //When
        var result = controller.GetIzdelki();
        //Then
        Assert.Single(result.Value);
        }

        [Fact]
        public void GetIzdelkiVrneNIzdelkovCeJeVDBNObjektov()
        {
        //Given
        var izdelek = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };
        var izdelek2 = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };
        dbContext.Izdelki.Add(izdelek);
        dbContext.Izdelki.Add(izdelek2);
        dbContext.SaveChanges();
        //When
        var result = controller.GetIzdelki();
        //Then
        Assert.Equal(2, result.Value.Count());
        }

        [Fact]
        public void GetIzdelkiVrneTocenTip()
        {
        //Given
        
        //When
        var result = controller.GetIzdelki();
        //Then
        Assert.IsType<ActionResult<IEnumerable<Izdelek>>>(result);
        }

        [Fact]
        public void GetIzdelkiVrneNullKoIDNeObstajaVDB()
        {
        //Given
            
        //When
        var result = controller.GetIzdelki(0);
        //Then
        Assert.Null(result.Value);
        }

        [Fact]
        public void GetIzdelkiVrni404KoIDNeObstajaVDB()
        {
        //Given
        
        //When
        var result = controller.GetIzdelki(0);
        //Then
        Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetIzdelkiVrnePravlenTip()
        {
        //Given
        var izdelek = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };
        dbContext.Izdelki.Add(izdelek);
        dbContext.SaveChanges();

        var IzdelekId = izdelek.Id;
        //When
        var result = controller.GetIzdelki(IzdelekId);
        //Then
        Assert.IsType<ActionResult<Izdelek>>(result);
        }

        [Fact]
        public void GetIzdelkiVrnePravilenResource()
        {
        //Given
        var izdelek = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };
        dbContext.Izdelki.Add(izdelek);
        dbContext.SaveChanges();

        var IzdelekId = izdelek.Id;
        //When
        var result = controller.GetIzdelki(IzdelekId);
        //Then
        Assert.Equal(IzdelekId,result.Value.Id);
        }

        [Fact]
        public void PostIzdelkiStObjektovSePovecaZa1KoJeUstrezenObjekt()
        {
        //Given
        var izdelek = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };

        var oldCount = dbContext.Izdelki.Count();
        //When
        var result = controller.PostIzdelki(izdelek);
        //Then
        Assert.Equal(oldCount+1, dbContext.Izdelki.Count());
        }

        [Fact]
        public void PostIzdelkiVrni201CreatedCeJeUstrezenObjekt()
        {
        //Given
        var izdelek = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };
        //When
        var result = controller.PostIzdelki(izdelek);
        //Then
        Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void PutIzdelki_AtributiPosodobljeni_KoJeUstrezenObjekt()
        {
        //Given
        var izdelek = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };
        dbContext.Izdelki.Add(izdelek);
        dbContext.SaveChanges();

        var IzdelekId = izdelek.Id;

        izdelek.ImeIzdelka = "Kos";
        //When
        controller.PutIzdelki(IzdelekId, izdelek);
        var result = dbContext.Izdelki.Find(IzdelekId);
        //Then
        Assert.Equal(izdelek.ImeIzdelka, result.ImeIzdelka);
        }

        [Fact]
        public void PutIzdelki_Vrni204_KoJeUstrezenObjekt()
        {
        //Given
        var izdelek = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };
        dbContext.Izdelki.Add(izdelek);
        dbContext.SaveChanges();

        var IzdelekId = izdelek.Id;

        izdelek.ImeIzdelka = "Kos";
        //When
        var result = controller.PutIzdelki(IzdelekId, izdelek);
        //Then
        Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void PutIzdelki_Vrni400_KoJeNapacenObjekt()
        {
        //Given
        var izdelek = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };
        dbContext.Izdelki.Add(izdelek);
        dbContext.SaveChanges();

        var IzdelekId = izdelek.Id + 1;

        izdelek.ImeIzdelka = "Kos";
        //When
        var result = controller.PutIzdelki(IzdelekId, izdelek);
        //Then
        Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutIzdelki_NeSpremeniAtributov_KoJeNapacenObjekt()
        {
        //Given
        var izdelek = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };
        dbContext.Izdelki.Add(izdelek);
        dbContext.SaveChanges();

        var izdelek2 = new Izdelek
            {
                Id = izdelek.Id,
                ImeIzdelka = "sdd",
                OpisIzdelka = "kfddfki",
                CenaIzdelka = 310,
                Zaloga = 120,
                NaVoljo = false
            };

        //When
        controller.PutIzdelki(izdelek.Id +1, izdelek2);
        var result = dbContext.Izdelki.Find(izdelek.Id);
        //Then
        Assert.Equal(izdelek.ImeIzdelka, result.ImeIzdelka);
        }

        [Fact]
        public void DeleteIzdelki_ZmanjsanjeStObjektov_KoJeVnesenUstrezenID()
        {
        //Given
        var izdelek = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };
        dbContext.Izdelki.Add(izdelek);
        dbContext.SaveChanges();

        var IzdelekId = izdelek.Id;
        var objCount = dbContext.Izdelki.Count();
        //When
        controller.DeleteIzdelki(IzdelekId);
        //Then
        Assert.Equal(objCount-1, dbContext.Izdelki.Count());
        }

        [Fact]
        public void DeleteIzdelki_Vrne200OK_KoJeVnesenUstrezenID()
        {
        //Given
        var izdelek = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };
        dbContext.Izdelki.Add(izdelek);
        dbContext.SaveChanges();

        var IzdelekId = izdelek.Id;
        //When
        var result = controller.DeleteIzdelki(IzdelekId);
        //Then
        Assert.Null(result.Result);
        }

        [Fact]
        public void DeleteIzdelki_Vrne404NotFound_KoJeVnesenNapacenID()
        {
        //Given
        
        //When
        var result = controller.DeleteIzdelki(-1);
        //Then
        Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void DeleteIzdelki_StObjektovSeNeZmanjsa_KoJeVnesenUstrezenID()
        {
        //Given
        var izdelek = new Izdelek
            {
                ImeIzdelka = "zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };
        dbContext.Izdelki.Add(izdelek);
        dbContext.SaveChanges();

        var IzdelekId = izdelek.Id;
        var objCount = dbContext.Izdelki.Count();
        //When
        var result = controller.DeleteIzdelki(IzdelekId+1);
        //Then
        Assert.Equal(objCount,dbContext.Izdelki.Count());
        }
    }
}