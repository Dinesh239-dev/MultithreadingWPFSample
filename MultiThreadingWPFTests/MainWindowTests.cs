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
        [TestMethod]
        public void MainWindowViewModel_WhenStartButtonClicked_ExecuteMethod()
        {
            // Arrange
            var sut = this.Create();

            // Act
            sut.TaskExecutionOrder();

            // Arrange
            sut.TaskCount.ShouldBeGreaterThan(0);

        }
        
        private MainWindowViewModel Create()
        {
            return new MainWindowViewModel();
        }
    }
}