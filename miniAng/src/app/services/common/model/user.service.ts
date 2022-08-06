import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, firstValueFrom } from 'rxjs';
import { Create_User } from './../../../contracts/users/create_user';
import { HttpClientService } from 'src/app/services/common/http-client.service';
import { Injectable } from '@angular/core';
import { User } from 'src/app/entities/user';
import { TokenResponse } from 'src/app/contracts/token/tokenResponse';
import { SocialUser } from '@abacritt/angularx-social-login';
@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private httpClientService: HttpClientService,
    private toastrService: ToastrService,
    private router: Router
  ) {}

  async create(user: User): Promise<Create_User> {
    const observable: Observable<Create_User | User> =
      this.httpClientService.post<Create_User | User>(
        {
          controller: 'users',
        },
        user
      );

    return (await firstValueFrom(observable)) as Create_User;
  }
}
