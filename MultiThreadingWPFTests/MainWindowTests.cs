namespace WpfApp2.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WpfApp2;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Shouldly;
    using MultiThreadingWPF;

    [TestClass()]
    public class MainWindowTests
    {
        [TestMethod]
        public void MainWindowViewModel_InitializeContructor_AlwaysCreatesTheObject()
        {
            // Arrange
            var sut = this.Create();

            // Act & Assert
            sut.ShouldNotBeNull();
            
        }
        
        private MainWindowViewModel Create()
        {
            return new MainWindowViewModel();
        }
    }
}