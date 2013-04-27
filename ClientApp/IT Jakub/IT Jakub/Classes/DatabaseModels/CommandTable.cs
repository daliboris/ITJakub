﻿using IT_Jakub.Classes.Exceptions;
using IT_Jakub.Classes.Models;
using IT_Jakub.Classes.Networking;
using IT_Jakub.Classes.Utils;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IT_Jakub.Classes.DatabaseModels {
    class CommandTable {

        private static MobileService ms = MobileService.getInstance();
        private static MobileServiceClient msc = MobileService.getMobileServiceClient();
        private IMobileServiceTable<Command> table = msc.GetTable<Command>();

        internal async Task<long> createCommand(Session s, User u, string commandText) {
            try {
                Command c = new Command {
                    UserId = u.Id,
                    CommandText = commandText,
                    SessionId = s.Id
                };
                await table.InsertAsync(c);
                List<Command> command = await table
                    .Where(Item => Item.CommandText == c.CommandText)
                    .Where(Item => Item.UserId == c.UserId)
                    .Where(Item => Item.SessionId == c.SessionId).ToListAsync();
                long id = command[command.Count - 1].Id;
                return id;
            } catch (Exception e) {
                throw new ServerErrorException(e);
            }
        }

        internal async Task<List<Command>> getAllSessionCommands(Session s) {
            List<Command> items;
            try {
                items = await table.Where(Item => Item.SessionId == s.Id).ToListAsync();
            } catch (Exception e) {
                throw new ServerErrorException(e);
            }
            return items;
        }

        internal async Task<List<Command>> getAllCommands() {
            List<Command> items;
            try {
                items = await table.ToListAsync();
            } catch (Exception e) {
                throw new ServerErrorException(e);
            }
            return items;
        }


        private async void deleteCommand(Command c) {
            try {
                await table.DeleteAsync(c);
            } catch (Exception e) {
                throw new ServerErrorException(e);
            }
        }

        internal async Task<bool> removeSessionsCommand(Session s) {
            List<Command> items;
            try {
                items = await table.Where(Item => Item.SessionId == s.Id).ToListAsync();
                for (int i = 0; i < items.Count; i++) {
                    deleteCommand(items[i]);
                }
            } catch (Exception e) {
                throw new ServerErrorException(e);
            }
            return true;
        }


        internal async Task<List<Command>> getAllNewSessionCommands(SignedSession s) {
            List<Command> items;
            try {
                items = await table.Where(Item => Item.SessionId == s.getSessionData().Id).Where(Item => Item.Id > s.getLatestCommandId()).ToListAsync();
            } catch (Exception e) {
                throw new ServerErrorException(e);
            }
            return items;
        }


        internal async void deletePrevMoveCommands(long latestCommandId, Session s) {
            List<Command> items;
            try {
                items = await table.Take(150).Where(Item => Item.SessionId == s.Id).Where(Item => Item.Id < latestCommandId).Where(Item => Item.CommandText.Contains("Move(")).ToListAsync();
                if (items.Count > 0) {
                    LinkedList<Command> l = new LinkedList<Command>(items);
                    for (LinkedListNode<Command> ln = l.First; ln != l.Last.Next; ln = ln.Next) {
                        this.deleteCommand(ln.Value);
                    }
                }
            } catch (Exception e) {
                throw new ServerErrorException(e);
            }
        }

        internal async Task removeUserLoginLogoutCommands(Session s, User u) {
            List<Command> loginItems;
            try {
                loginItems = await table.Where(Item => Item.SessionId == s.Id).Where(Item => Item.CommandText.Contains("Login(" + u.Id + ")")).ToListAsync();
                if (loginItems.Count > 0) {
                    for (int i = 0; i < loginItems.Count; i++) {
                        this.deleteCommand(loginItems[i]);
                    }
                }
            } catch (Exception e) {
                throw new ServerErrorException(e);
            }

            List<Command> logoutItems;
            try {
                logoutItems = await table.Where(Item => Item.SessionId == s.Id).Where(Item => Item.CommandText.Contains("Logout(" + u.Id + ")")).ToListAsync();

                if (logoutItems.Count > 0) {
                    for (int i = 0; i < logoutItems.Count; i++) {
                        this.deleteCommand(logoutItems[i]);
                    }
                }
            } catch (Exception e) {
                throw new ServerErrorException(e);
            }
        }

        internal async Task deletePrevPromoteDemoteCommands(Session s) {
            List<Command> promoteItems;
            try {
                promoteItems = await table.Where(Item => Item.SessionId == s.Id).Where(Item => Item.CommandText.Contains("Promote(")).ToListAsync();

                if (promoteItems.Count > 0) {
                    for (int i = 0; i < promoteItems.Count - 1; i++) {
                        this.deleteCommand(promoteItems[i]);
                    }
                }
            } catch (Exception e) {
                throw new ServerErrorException(e);
            }

            List<Command> demoteItems;
            try {
                demoteItems = await table.Where(Item => Item.SessionId == s.Id).Where(Item => Item.CommandText.Contains("Demote(")).ToListAsync();
                if (demoteItems.Count > 0) {
                    for (int i = 0; i < demoteItems.Count - 1; i++) {
                        this.deleteCommand(demoteItems[i]);
                    }
                }
            } catch (Exception e) {
                throw new ServerErrorException(e);
            }
        }
    }
}
