//using BotKeeper.Service.Core.Models.Users;
//using BotKeeper.Service.Interfaces;
//using BotKeeper.Service.Models;
//using BotKeeper.Service.Models.Messages;
//using BotKeeper.Service.Models.Users;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace BotKeeper.Service.Core.Models {
//	internal class WelcomeInteraction : IInteraction {
//		public InteractionTypes Type { get; set; } = InteractionTypes.New;
//		public IServiceFactory serviceFactory;
//		public BaseUser User { get; set; }

//		protected Dictionary<Type, Func<IMessage, Task<IntercationResponse>>> messageGenerators
//			= new Dictionary<Type, Func<IMessage, Task<IntercationResponse>>>();

//		public WelcomeInteraction() {
//			messageGenerators.Add(typeof(NewUser), GetNextMessage);
//			messageGenerators.Add(typeof(UnknownUser), GetNextMessage);
//			messageGenerators.Add(typeof(User), GetNextMessage);
//			messageGenerators.Add(typeof(Admin), GetNextMessage);
//		}

//		public async Task<IntercationResponse> GenerateAnswer(IMessage message) {
//			await Task.Yield();


//			return Type switch
//			{
//				InteractionTypes.New => User switch
//				{
//					NewUser _ => new IntercationResponse { 
//						Text = $@"Hello {nameof(NewUser)}. I'm bot... type \help for help..."
//					},
//					UnknownUser _ => new IntercationResponse {
//						Text = $@"Hello {nameof(UnknownUser)}. I'm bot... type \help for help..."
//					},
//					User _ => new IntercationResponse {
//						Text = $@"Hello {nameof(User)}. I'm bot... type \help for help..."
//					},
//					Admin _ => new IntercationResponse {
//						Text = $@"Hello {nameof(Admin)}. I'm bot... type \help for help..."
//					},
//					_ => throw new NotImplementedException($"Unknown user type {User.GetType()}")
//				},
//				InteractionTypes.Сontinued => User switch
//				{
//					NewUser _ => await messageGenerators[typeof(NewUser)].Invoke(message),
//					UnknownUser _ => await messageGenerators[typeof(UnknownUser)].Invoke(message),
//					User _ => await messageGenerators[typeof(User)].Invoke(message),
//					Admin _ => await messageGenerators[typeof(Admin)].Invoke(message),
//					_ => throw new NotImplementedException($"Unknown user type {User.GetType()}")
//				},
//				_ => throw new InvalidOperationException($"Unknown type of interaction {Type.ToString()}"),
//			};
//		}

//		private async Task<IntercationResponse> GetNextMessage(IMessage message) {
//			return message switch
//			{
//				TextMessage textMessage => await ComputeCommandAndGetResult(textMessage),
//				_ => throw new NotImplementedException($"Unknown message type {message.GetType()}"),
//			};
//		}

//		private async Task<IntercationResponse> ComputeCommandAndGetResult(TextMessage message) {
//			await Task.Yield();
//			var text = message.Text.Trim().ToLowerInvariant();

//			return text switch
//			{
//				AllInternalCommands.Help => 
//					new IntercationResponse { 
//						Text = "Help information ..."
//					},
//				AllInternalCommands.Register =>  
//					new IntercationResponse {
//						Text = "Registration: type your new password."
//					},
//				_ => new IntercationResponse { Text = "type help  for more information" },
//			};
//		}
//	}
//}
