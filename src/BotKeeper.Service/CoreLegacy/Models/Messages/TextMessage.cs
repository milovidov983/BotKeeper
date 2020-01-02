using BotKeeper.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotKeeper.Service.Models.Messages {
	internal class TextMessage : IMessage {
		private string text;
		private long id;
		private long chatId;

		public string Text { get => text; }
		public long Id { get => id; }
		public long ChatId { get => chatId; }

		public TextMessage(string text, long chatId, int messageId) {
			this.text = text;
			this.chatId = chatId;
			this.id = messageId;
		}
	}
}
