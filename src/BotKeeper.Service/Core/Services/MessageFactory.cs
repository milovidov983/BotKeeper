using BotKeeper.Service.Interfaces;
using BotKeeper.Service.Models.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace BotKeeper.Service.Services {
	internal class MessageFactory: IMessageFactory {
		public IMessage Create(MessageEventArgs msg) {

			switch (msg?.Message?.Type) {
				case Telegram.Bot.Types.Enums.MessageType.Unknown:
					break;
				case Telegram.Bot.Types.Enums.MessageType.Text:
					return new TextMessage(msg.Message.Text, msg.Message.Chat.Id, msg.Message.MessageId);
				case Telegram.Bot.Types.Enums.MessageType.Photo:
					break;
				case Telegram.Bot.Types.Enums.MessageType.Audio:
					break;
				case Telegram.Bot.Types.Enums.MessageType.Video:
					break;
				case Telegram.Bot.Types.Enums.MessageType.Voice:
					break;
				case Telegram.Bot.Types.Enums.MessageType.Document:
					break;
				case Telegram.Bot.Types.Enums.MessageType.Sticker:
					break;
				case Telegram.Bot.Types.Enums.MessageType.Location:
					break;
				case Telegram.Bot.Types.Enums.MessageType.Contact:
					break;
				case Telegram.Bot.Types.Enums.MessageType.Venue:
					break;
				case Telegram.Bot.Types.Enums.MessageType.Game:
					break;
				case Telegram.Bot.Types.Enums.MessageType.VideoNote:
					break;
				case Telegram.Bot.Types.Enums.MessageType.Invoice:
					break;
				case Telegram.Bot.Types.Enums.MessageType.SuccessfulPayment:
					break;
				case Telegram.Bot.Types.Enums.MessageType.WebsiteConnected:
					break;
				case Telegram.Bot.Types.Enums.MessageType.ChatMembersAdded:
					break;
				case Telegram.Bot.Types.Enums.MessageType.ChatMemberLeft:
					break;
				case Telegram.Bot.Types.Enums.MessageType.ChatTitleChanged:
					break;
				case Telegram.Bot.Types.Enums.MessageType.ChatPhotoChanged:
					break;
				case Telegram.Bot.Types.Enums.MessageType.MessagePinned:
					break;
				case Telegram.Bot.Types.Enums.MessageType.ChatPhotoDeleted:
					break;
				case Telegram.Bot.Types.Enums.MessageType.GroupCreated:
					break;
				case Telegram.Bot.Types.Enums.MessageType.SupergroupCreated:
					break;
				case Telegram.Bot.Types.Enums.MessageType.ChannelCreated:
					break;
				case Telegram.Bot.Types.Enums.MessageType.MigratedToSupergroup:
					break;
				case Telegram.Bot.Types.Enums.MessageType.MigratedFromGroup:
					break;
				case Telegram.Bot.Types.Enums.MessageType.Animation:
					break;
				case Telegram.Bot.Types.Enums.MessageType.Poll:
					break;
			}

			throw new NotImplementedException($"Unknown message type {msg?.Message?.Type}");
		}
	}
}