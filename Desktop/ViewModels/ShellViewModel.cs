﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Desktop.EventModels;

namespace Desktop.ViewModels
{
    public class ShellViewModel : Conductor<object> , IHandle<LogOnEventModel>
    {
        //private LoginViewModel _loginViewModel;
        private IEventAggregator _eventAggregator;
        private SimpleContainer _simpleContainer;
        private RegistrationViewModel _shellViewModel;
        private PassViewModel _passViewModel;

        public ShellViewModel( RegistrationViewModel shellViewModel , IEventAggregator eventAggregator
            , SimpleContainer simpleContainer, PassViewModel passViewModel)
        {
            _eventAggregator = eventAggregator;
            _simpleContainer = simpleContainer;
            _shellViewModel = shellViewModel;
            _passViewModel = passViewModel;
            //_loginViewModel = loginViewModel;
            _eventAggregator.Subscribe(this);
            ActivateItem(_simpleContainer.GetInstance<LoginViewModel>());                                                               
        }

        public void Handle(LogOnEventModel message)
        {
            ActivateItem(_passViewModel);
        }
    }
}
