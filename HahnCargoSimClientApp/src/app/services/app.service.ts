import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  public user: any = null;

  constructor(private router: Router, private toastr: ToastrService, public httpClient: HttpClient) { }

  async loginByAuth({ username, password }) {
    try {
      console.log('username', username)
      await this.authLogin(username, password);
      await this.getProfile();
      this.router.navigate(['/']);
      this.toastr.success('Login success');
    } catch (error) {
      this.toastr.error(error.message);
    }
  }

  async getProfile() {
    try {
      const user = await this.getAuthStatus();
      if (user) {
        this.user = user;
      } else {
        this.logout();
      }
    } catch (error) {
      this.logout();
      throw error;
    }
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('gatekeeper_token');
    this.user = null;
    this.router.navigate(['/login']);
  }

  authLogin = async (username: string, password: string) => {
    debugger
    return this.httpClient.post(`${environment.baseUrl}/User/Login`, { username, password })
      .subscribe((result: any) => {
        localStorage.setItem(
          'authentication',
          JSON.stringify({ profile: { username: username, token: result.token } })
        );
        return { profile: { username: username, token: result.token, } };
      })
    return new Promise(async (res, rej) => {
      if (password === 'Hahn') {
        localStorage.setItem(
          'authentication',
          JSON.stringify({ profile: { username: username, email: 'admin@example.com' } })
        );
        return res({ profile: { username: username, email: 'admin@example.com' } });
      }
      return rej({ message: 'Credentials are wrong!' });
    });
  };

  getAuthStatus = async () => {
    return new Promise(async (res, rej) => {
      try {
        let authentication = localStorage.getItem('authentication');
        if (authentication) {
          authentication = JSON.parse(authentication);
          return res(authentication);
        }
        return res(undefined);
      } catch (error) {
        return res(undefined);
      }
    });
  };
}
