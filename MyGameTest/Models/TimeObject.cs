﻿using GalaSoft.MvvmLight;
using System;

namespace MyGameTest.Models
{
    public abstract class TimeObject : ViewModelBase
    {
        public TimeObject()
        {
            var startTime = DateTime.Now;            
            _updateAction = (d)=> StartBase(startTime);
            Time.Instance.OnUpdate += UpdateBase;
        }

        private Action<double> _updateAction;

        private void StartBase(DateTime startTime)
        {
            Start((DateTime.Now - startTime).TotalSeconds);
            _updateAction = Update;
        }
        private void UpdateBase(double deltaTime)
        {
            _updateAction(deltaTime);
        }

        protected virtual void Update(double deltaTime) { }
        protected virtual void Start(double deltaTime) { }
    }
}
