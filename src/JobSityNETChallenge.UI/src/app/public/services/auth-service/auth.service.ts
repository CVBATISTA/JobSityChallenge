import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserI } from 'src/app/model/user.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private snackbar: MatSnackBar) {}

  login(user: UserI): Observable<string> {
    return this.http
      .post('api/Account/login', user, { responseType: 'text' })
      .pipe(
        tap((e) => {
          localStorage.setItem('nestjs_chat_app', e);
          this.http
            .get('api/Account/get-user-id', {
              responseType: 'text',
              headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${localStorage.getItem(
                  'nestjs_chat_app'
                )}`,
              },
            })
            .toPromise()
            .then((p: string) => {
              localStorage.setItem('userId', p);
            });
        }),
        tap(() =>
          this.snackbar.open('Login Successfull', 'Close', {
            duration: 2000,
            horizontalPosition: 'right',
            verticalPosition: 'top',
          })
        )
      );
  }

  create(user: UserI): Observable<string> {
    user.confirmPassword = user.password;
    return this.http
      .post('https://localhost:7009/api/Account/register', user, {
        responseType: 'text',
      })
      .pipe(
        tap(() =>
          this.snackbar.open(`User created successfully`, 'Close', {
            duration: 2000,
            horizontalPosition: 'right',
            verticalPosition: 'top',
          })
        ),
        catchError((e) => {
          this.snackbar.open(
            `User could not be created, due to: ${e.error.message}`,
            'Close',
            {
              duration: 5000,
              horizontalPosition: 'right',
              verticalPosition: 'top',
            }
          );
          return throwError(e);
        })
      );
  }
}
