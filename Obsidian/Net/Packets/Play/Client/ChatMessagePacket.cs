﻿using Obsidian.API;
using Obsidian.Chat;
using Obsidian.Entities;
using Obsidian.Serializer.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Obsidian.Net.Packets.Play.Client
{
    public partial class ChatMessagePacket : IPacket
    {
        [Field(0)]
        public ChatMessage Message { get; private set; }

        [Field(1)]
        public sbyte Position { get; private set; } // 0 = chatbox, 1 = system message, 2 = game info (actionbar)

        [Field(2), FixedLength(2)]
        public List<long> Sender { get; private set; } = new List<long>
        {
            0, 0
        };

        public int Id => 0x0E;

        public ChatMessagePacket() { }

        public ChatMessagePacket(ChatMessage message, MessageType type, Guid sender)
        {
            this.Message = message;
            this.Position = (sbyte)type;
            //this.Sender = sender;
        }

        public Task WriteAsync(MinecraftStream stream) => Task.CompletedTask;

        public Task ReadAsync(MinecraftStream stream) => Task.CompletedTask;

        public Task HandleAsync(Obsidian.Server server, Player player) => Task.CompletedTask;
    }
}