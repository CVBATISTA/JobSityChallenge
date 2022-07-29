import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MessageI } from 'src/app/model/message.interface';
import { SignalrService } from 'src/app/public/services/signalr.service';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  constructor(
    private snackbar: MatSnackBar,
    private http: HttpClient,
    private signalR: SignalrService
  ) {}

  sendMessage(message: MessageI) {
    this.signalR.sendMessage(message.room.id, message.text);
  }

  getMessages(id, date?): Promise<Object> {
    let lastDate = '';
    if (!!date) lastDate = `?lastMessageTime=${date}`;
    return this.http
      .get(
        `https://localhost:7009/api/chat-room/${id}/chat-messages` + lastDate,
        {
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${localStorage.getItem('nestjs_chat_app')}`,
          },
        }
      )
      .toPromise();
  }

  getMyRooms(): Promise<object> {
    return this.http
      .get('https://localhost:7009/api/chat-room-management', {
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${localStorage.getItem('nestjs_chat_app')}`,
        },
      })
      .toPromise();
  }

  async createRoom(room): Promise<boolean> {
    let x = await this.http
      .post('https://localhost:7009/api/chat-room-management', room, {
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${localStorage.getItem('nestjs_chat_app')}`,
        },
      })
      .toPromise()
      .catch((e) => {
        return false;
      })
      .then(() => {
        this.snackbar.open(`Room ${room.name} created successfully`, 'Close', {
          duration: 2000,
          horizontalPosition: 'right',
          verticalPosition: 'top',
        });
        return true;
      });
    return true;
  }
}
