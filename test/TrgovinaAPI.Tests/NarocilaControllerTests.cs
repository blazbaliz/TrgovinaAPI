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
    public class NarocilaControllerTests : IDisposable 
    {
        DbContextOptionsBuilder<NarocilaContext> optionsBuilder;
        NarocilaContext dbContext;
        NarocilaController controller;
        Narocilo narocilo;

        public NarocilaControllerTests()
        {
            optionsBuilder = new DbContextOptionsBuilder<NarocilaContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestInMemDB");
            dbContext = new NarocilaContext(optionsBuilder.Options);
            controller = new NarocilaController(dbContext);

            narocilo = new Narocilo
            {
                UporabnikId = 1,
                IzdelekId = 1,
                Kolicina = 1,
                Status = "Odprto"
            };
        }

        public void Dispose()
        {
            optionsBuilder = null;
            foreach(var narocilo in dbContext.Narocila)
            {
                dbContext.Narocila.Remove(narocilo);
            }
            dbContext.SaveChanges();
            dbContext.Dispose();
            controller = null;
            narocilo = null;
        }

        [Fact]
        public void GetNarocila_ReturnNoneObjectWhenDBisEmpty()
        {
        //Given
        
        //When
        var result = controller.GetNarocila();
        //Then
        Assert.Empty(result.Value);
        }

        [Fact]
        public void GetNarocila_Return1Object_WhenDBHasOneItem()
        {
        //Given
        dbContext.Narocila.Add(narocilo);
        dbContext.SaveChanges();
        //When
        var result = controller.GetNarocila();
        //Then
        Assert.Single(result.Value);
        }

        [Fact]
        public void GetNarocila_ReturnNItems_WhenDBHasNObjects()
        {
        //Given
        Narocilo narocilo2 = new Narocilo
        {
            UporabnikId = 2,
            IzdelekId = 3,
            Kolicina = 2,
            Status = "Odprto"
        };
        dbContext.Narocila.Add(narocilo);
        dbContext.Narocila.Add(narocilo2);
        dbContext.SaveChanges();
        //When
        var result = controller.GetNarocila();
        //Then
        Assert.Equal(2, result.Value.Count());
        }

        [Fact]
        public void GetNarocila_ReturnCorrectType()
        {
        //Given
        
        //When
        var result = controller.GetNarocila();
        //Then
        Assert.IsType<ActionResult<IEnumerable<Narocilo>>>(result);
        }

        [Fact]
        public void GetNarocila_ReturnsNullResult_WhenInvalidId()
        {
        //Given

        //When
        var result = controller.GetNarocila(0);
        //Then
        Assert.Null(result.Value);
        }

        [Fact]
        public void GetNarocila_Return404NotFound_WhenInvalidId()
        {
        //Given
        
        //When
        var result = controller.GetNarocila(0);
        //Then
        Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetNarocila_ReturnTheCorrectType()
        {
        //Given
        dbContext.Narocila.Add(narocilo);
        dbContext.SaveChanges();
        
        var NarociloId = narocilo.Id;
        //When
        var result = controller.GetNarocila(NarociloId);
        //Then
        Assert.IsType<ActionResult<Narocilo>>(result);
        }

        [Fact]
        public void GetNarocila_ReturnCorrectResource()
        {
        //Given
        dbContext.Narocila.Add(narocilo);
        dbContext.SaveChanges();
        
        var NarociloId = narocilo.Id;
        //When
        var result = controller.GetNarocila(NarociloId);
        //Then
        Assert.Equal(narocilo.Id, result.Value.Id);
        }

        [Fact]
        public void PostNarocila_ObjectIncrement_WhenValidObject()
        {
        //Given
        var OldCount = dbContext.Narocila.Count();
        //When
        var result = controller.PostNarocila(narocilo);
        //Then
        Assert.Equal(OldCount + 1, dbContext.Narocila.Count());
        }

        [Fact]
        public void PostNarocila_AttributeUpdated_WhenValidObject()
        {
        //Given
        dbContext.Narocila.Add(narocilo);
        dbContext.SaveChanges();

        var NarociloId = narocilo.Id;
        
        narocilo.Kolicina = 5;
        //When
        controller.PutNarocila(NarociloId, narocilo);
        var result = dbContext.Narocila.Find(NarociloId);
        //Then
        Assert.Equal(narocilo.Kolicina, result.Kolicina);
        }

        [Fact]
        public void PostNarocila_Return204_WhenValidObject()
        {
        //Given
        dbContext.Narocila.Add(narocilo);
        dbContext.SaveChanges();

        var NarociloId = narocilo.Id;
        //When
        var result = controller.PutNarocila(NarociloId, narocilo);
        //Then
        Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public void PostNarocila_Return400_WhenInvalidObject()
        {
        //Given
        dbContext.Narocila.Add(narocilo);
        dbContext.SaveChanges();

        var NarociloId = narocilo.Id + 1;
        narocilo.Kolicina = 23;
        //When
        var result = controller.PutNarocila(NarociloId, narocilo);
        //Then
        Assert.IsType<BadRequestResult>(result);
        }


        [Fact]
        public void PostNarocila_AttributeUnchanged_WhenInvalidObject()
        {
        //Given
        dbContext.Narocila.Add(narocilo);
        dbContext.SaveChanges();

        Narocilo narocilo2 = new Narocilo
        {
            Id = narocilo.Id,
            UporabnikId = 2,
            IzdelekId = 1,
            Kolicina = 2,
            Status = "Zakljuceno"
        };
        //When
        controller.PutNarocila(narocilo.Id +1, narocilo2);
        var result = dbContext.Narocila.Find(narocilo.Id);
        //Then
        Assert.Equal(narocilo.Kolicina, result.Kolicina);
        }

        [Fact]
        public void DeleteNarocila_ObjectDecrement_WhenValidObjectID()
        {
        //Given
        dbContext.Narocila.Add(narocilo);
        dbContext.SaveChanges();

        var NarociloId = narocilo.Id;
        var objCount = dbContext.Narocila.Count();
        //When
        controller.DeleteNarocila(NarociloId);
        //Then
        Assert.Equal(objCount - 1, dbContext.Narocila.Count());
        }

        [Fact]
        public void DeleteNarocila_Return200Ok_WhenValidObjectID()
        {
        //Given
        
        //When
        
        //Then
        }

        [Fact]
        public void DeleteNarocila_Return400NotFound_WhenInvalidObjectID()
        {
        //Given
        
        //When
        
        //Then
        }

        [Fact]
        public void DeleteNarocila_ObjectCountNotDecrement_WhenInvalidObjectID()
        {
        //Given
        
        //When
        
        //Then
        }

    }
}