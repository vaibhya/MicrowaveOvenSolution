using MicrowaveOven.Service.Impl;

namespace MicrowaveOven.Test.Service
{
    [TestClass]
    public class TimerServiceTest
    {
        private TimerService _timerService;

        [TestInitialize]
        public void Setup()
        {
            _timerService = new TimerService();
        }

        [TestMethod]
        public void AddTime_ShouldIncreaseRemainingSeconds()
        {
            _timerService.AddTime(30);
            Assert.AreEqual(30, _timerService.GetTime());
        }

        [TestMethod]
        public void Start_ShouldSetInitialTimeTo60()
        {
            _timerService.Start();
            Assert.AreEqual(60, _timerService.GetTime());
        }

        [TestMethod]
        public void Reset_ShouldSetRemainingSecondsToZero()
        {
            _timerService.AddTime(30);
            _timerService.Reset();
            Assert.AreEqual(0, _timerService.GetTime());
        }

        [TestMethod]
        public async Task TimerTick_ShouldInvokeIsTimeOverEvent_WhenTimeIsUp()
        {
            var resetEvent = new ManualResetEventSlim(false);
            _timerService.AddTime(1); // 1 second

            _timerService.IsTimeOver += (isOver) =>
            {
                if (isOver)
                    resetEvent.Set();
            };

            // Wait max 3 seconds for timer to trigger
            bool signaled = resetEvent.Wait(3000);
            Assert.IsTrue(signaled, "IsTimeOver event was not triggered.");
        }

        [TestMethod]
        public void Stop_ShouldPauseTimer()
        {
            _timerService.AddTime(5);
            _timerService.Stop();
            int timeBefore = _timerService.GetTime();

            Thread.Sleep(2000); // Wait 2 seconds
            int timeAfter = _timerService.GetTime();

            Assert.AreEqual(timeBefore, timeAfter, "Timer should not decrease after Stop.");
            

        }
    }
}
