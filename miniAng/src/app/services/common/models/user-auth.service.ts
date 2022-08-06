import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpClientService } from 'src/app/services/common/http-client.service';
import { Injectable } from '@angular/core';
import { firstValueFrom, Observable } from 'rxjs';
import { TokenResponse } from 'src/app/contracts/token/tokenResponse';
import { SocialUser } from '@abacritt/angularx-social-login';

@Injectable({
  providedIn: 'root',
})
export class UserAuthService {
  constructor(
    private httpClientService: HttpClientService,
    private toastrService: ToastrService,
    private router: Router
  ) {}

  async login(
    usernameOrEmail: string,
    password: string,
    callBackFunction?: () => void
  ): Promise<void> {
    const observable: Observable<any | TokenResponse> =
      this.httpClientService.post<any | TokenResponse>(
        {
          controller: 'auth',
          action: 'login',
        },
        { usernameOrEmail, password }
      );
    const tokenResponse: TokenResponse = (await firstValueFrom(
      observable
    )) as TokenResponse;

    if (tokenResponse) {
      localStorage.setItem('accessToken', tokenResponse.token.accessToken);

      this.toastrService.success('Success');
      this.router.navigate(['']);
    }

    callBackFunction();
  }

  async googleLogin(user: SocialUser, callBackFunction?: () => void) {
    const observable: Observable<SocialUser | TokenResponse> =
      this.httpClientService.post<SocialUser | TokenResponse>(
        {
          action: 'google-login',
          controller: 'auth',
        },
        user
      );

    const tokenResponse: TokenResponse = (await firstValueFrom(
      observable
    )) as TokenResponse;

    if (tokenResponse) {
      localStorage.setItem('accessToken', tokenResponse.token.accessToken);
      this.toastrService.success('Success');
      this.router.navigate(['']);
    } else {
      this.toastrService.error('Error');
    }

    callBackFunction();
  }
}
