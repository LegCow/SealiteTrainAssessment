using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sealite.TrainAssessment;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sealite.Tests
{
    [TestClass]
    public class AssessmentTests
    {
        private TrainNetwork _network;
        private IRoutePlanner _planner;
        private Town _townA;
        private Town _townB;
        private Town _townC;
        private Town _townD;
        private Town _townE;

        [TestInitialize]
        public async Task Initialize()
        {
            var repository = new TrainNetworkFileRepository(@"C:\Dev\SealiteTrainAssessment\Sealite.TrainAssessment\Input.txt"); // TODO
            _network = await repository.LoadFromFile();
            _planner = new SimpleRecursiveRoutePlanner();
            _townA = _network.GetTown('A');
            _townB = _network.GetTown('B');
            _townC = _network.GetTown('C');
            _townD = _network.GetTown('D');
            _townE = _network.GetTown('E');
        }

        [TestMethod]
        public void Test_1()
        {
            //Test #1: The distance of the route A=>B=>C is 9
            var distance = _townA.TravelTo(_townB).TravelTo(_townC).TotalLength;

            Assert.AreEqual(distance, 9);
        }

        [TestMethod]
        public void Test_2()
        {
            //Test #2: The distance of the route A=>D is 5
            var distance = _townA.TravelTo(_townD).TotalLength;

            Assert.AreEqual(distance, 5);
        }

        [TestMethod]
        public void Test_3()
        {
            //Test #3: The distance of the route A=>D=>C is 13
            var distance = _townA.TravelTo(_townD).TravelTo(_townC).TotalLength;

            Assert.AreEqual(distance, 13);
        }

        [TestMethod]
        public void Test_4()
        {
            //Test #4: The distance of the route A=>E=>B=>C=>D is 22
            var distance = _townA.TravelTo(_townE).TravelTo(_townB).TravelTo(_townC).TravelTo(_townD).TotalLength;

            Assert.AreEqual(distance, 22);
        }

        [TestMethod]
        public void Test_5()
        {
            //Test #5: Route A=>E=>D doesn't exist
            void routeAED() => _townA.TravelTo(_townE).TravelTo(_townD);
            Assert.ThrowsException<InvalidOperationException>(routeAED);
        }

        [TestMethod]
        public void Test_6()
        {
            //Test #6: Number of trips from C to C with maximum 3 stops is 2 ( C=>D=>C, C=>E=>B=>C )
            var routes = _planner.CreateRoutes(_townC, 4)
                .Where(r => r.EndTown == _townC)
                .Where(r => r.TotalStops <= 3)
                .ToList();

            var routeCDC = _townC.TravelTo(_townD).TravelTo(_townC);
            var routeCEBC = _townC.TravelTo(_townE).TravelTo(_townB).TravelTo(_townC);

            Assert.AreEqual(routes.Count, 2);
            Assert.IsTrue(routes.Contains(routeCDC));
            Assert.IsTrue(routes.Contains(routeCEBC));
        }

        [TestMethod]
        public void Test_7()
        {
            //Test #7: Number of trips from A to C with exactly 4 stops is 3 ( A=>B=>C=>D=>C, A=>D=>C=>D=>C, A=>D=>E=>B=>C )
            var routes = _planner.CreateRoutes(_townA, 4)
                .Where(r => r.EndTown == _townC)
                .Where(r => r.TotalStops == 4)
                .ToList();

            var routeABCDC = _townA.TravelTo(_townB).TravelTo(_townC).TravelTo(_townD).TravelTo(_townC);
            var routeADCDC = _townA.TravelTo(_townD).TravelTo(_townC).TravelTo(_townD).TravelTo(_townC);
            var routeADEBC = _townA.TravelTo(_townD).TravelTo(_townE).TravelTo(_townB).TravelTo(_townC);

            Assert.AreEqual(routes.Count, 3);
            Assert.IsTrue(routes.Contains(routeABCDC));
            Assert.IsTrue(routes.Contains(routeADCDC));
            Assert.IsTrue(routes.Contains(routeADEBC));
        }

        [TestMethod]
        public void Test_8()
        {
            //Test #8: The length of the shortest route from A to C is 9 ( A=>B=>C )
            var route = _planner.CreateRoutes(_townA, 5)
                .Where(r => r.EndTown == _townC)
                .OrderBy(r => r.TotalLength)
                .First();

            var routeABC = _townA.TravelTo(_townB).TravelTo(_townC);

            Assert.IsTrue(route.Equals(routeABC));
        }

        [TestMethod]
        public void Test_9()
        {
            //Test #9: The length of the shortest route from B to B is 9 ( B=>C=>E=>B )
            var route = _planner.CreateRoutes(_townB, 5)
                .Where(r => r.EndTown == _townB)
                .OrderBy(r => r.TotalLength)
                .First();

            var routeBCEB = _townB.TravelTo(_townC).TravelTo(_townE).TravelTo(_townB);

            Assert.IsTrue(route.Equals(routeBCEB));
        }

        [TestMethod]
        public void Test_10()
        {
            //Test #10: The number of trips from C to C with distance less than 30 is 7 ( C=>D=>C, C=>D=>C=>E=>B=>C, C=>D=>E=>B=>C, C=>E=>B=>C, C=>E=>B=>C=>D=>C, C=>E=>B=>C=>E=>B=>C, C=>E=>B=>C=>E=>B=>C=>E=>B=>C )
            var routes = _planner.CreateRoutes(_townC, 10)
                .Where(r => r.EndTown == _townC)
                .Where(r => r.TotalLength < 30)
                .ToList();

            var routeCDC = _townC.TravelTo(_townD).TravelTo(_townC);
            var routeCDCEBC = _townC.TravelTo(_townD).TravelTo(_townC).TravelTo(_townE).TravelTo(_townB).TravelTo(_townC);
            var routeCDEBC = _townC.TravelTo(_townD).TravelTo(_townE).TravelTo(_townB).TravelTo(_townC);
            var routeCEBC = _townC.TravelTo(_townE).TravelTo(_townB).TravelTo(_townC);
            var routeCEBCDC = _townC.TravelTo(_townE).TravelTo(_townB).TravelTo(_townC).TravelTo(_townD).TravelTo(_townC);
            var routeCEBCEBC = _townC.TravelTo(_townE).TravelTo(_townB).TravelTo(_townC).TravelTo(_townE).TravelTo(_townB).TravelTo(_townC);
            var routeCEBCEBCEBC = _townC.TravelTo(_townE).TravelTo(_townB).TravelTo(_townC).TravelTo(_townE).TravelTo(_townB).TravelTo(_townC).TravelTo(_townE).TravelTo(_townB).TravelTo(_townC);

            Assert.AreEqual(routes.Count, 7);
            Assert.IsTrue(routes.Contains(routeCDC));
            Assert.IsTrue(routes.Contains(routeCDCEBC));
            Assert.IsTrue(routes.Contains(routeCDEBC));
            Assert.IsTrue(routes.Contains(routeCEBC));
            Assert.IsTrue(routes.Contains(routeCEBCDC));
            Assert.IsTrue(routes.Contains(routeCEBCEBC));
            Assert.IsTrue(routes.Contains(routeCEBCEBCEBC));
        }
    }
}
