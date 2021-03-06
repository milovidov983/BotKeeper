﻿using BotKeeper.Service.Core.Factories;
using BotKeeper.Service.Core.Interfaces;
using BotKeeper.Service.Core.Interfaces.UsersInterfaces;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Core {
	internal class BotContext {
		private State currentState;
		private readonly IStorage storage;
		private State CurrentState {
			get => currentState;
			set {
				Commands.CurrentState = value;
				currentState = value;
			}
		}

		public readonly ISender Sender;
		public readonly ICommandHandlerFactory HandlerFactory;
		public readonly IUserServiceFactory UserServiceFactory;
		public readonly IValidationService ValidationService;
		public readonly MessageEventArgs Request;

		private readonly IStateFactory stateFactory;
		private long? userId;

		public CommandController Commands { get; }
		/// <summary>
		/// Main user context
		/// </summary>
		/// <param name="state">current state</param>
		/// <param name="serviceFactory">services</param>
		/// <param name="request">user request</param>
		/// <param name="userId">variable is not null if the state is cached</param>
		public BotContext(State state, IServiceFactory serviceFactory, MessageEventArgs request, long? userId = null) {
			this.userId = userId;
			Commands = new CommandController(state);
			storage = serviceFactory.Storage;
			UserServiceFactory = serviceFactory.UserServiceFactory;
			Sender = serviceFactory.SenderFactory.CreateSender(request);
			HandlerFactory = serviceFactory.HandlerFactory;
			stateFactory = serviceFactory.StateFactory;
			Request = request;
		}

		public async Task<BotContext> MakeTransitionTo(State state) {
			if (userId.HasValue) {
				await TransitionToAsync(state, userId.Value);
			} else {
				TransitionTo(state);
			}

			return this;
		}

		private async Task TransitionToAsync(State state, long userId) {
			CurrentState = state;
			await SaveCurrentUserState(state, userId);
			CurrentState.SetContext(this);
		}

		public async Task TransitionToAsync(Type stateType, long userId) {
			var currentState = stateFactory.Create(stateType);
			await TransitionToAsync(currentState, userId);
		}

		private void TransitionTo(State state) {
			CurrentState = state;
			CurrentState.SetContext(this);
		}
		private async Task SaveCurrentUserState(State state, long userId) {
			await storage.SetUserState(userId, state);
		}
	}
}
