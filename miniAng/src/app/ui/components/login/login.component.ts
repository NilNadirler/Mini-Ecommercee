import { UserAuthService } from './../../../services/common/models/user-auth.service';
import { AuthService } from './../../../services/common/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from './../../../services/common/model/user.service';
import { Component, OnInit } from '@angular/core';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { ActivatedRoute, Router } from '@angular/router';
import {
  FacebookLoginProvider,
  SocialAuthService,
  SocialUser,
} from '@abacritt/angularx-social-login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends BaseComponent implements OnInit {
  constructor(
    private userAuthService: UserAuthService,
    spinner: NgxSpinnerService,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private socialAuthService: SocialAuthService
  ) {
    super(spinner);
    socialAuthService.authState.subscribe(async (user: SocialUser) => {
      console.log(user);
      this.showSpinner(SpinnerType.BallAtom);
      await userAuthService.googleLogin(user, () => {
        this.authService.idendtityCheck();
        this.hideSpinner(SpinnerType.BallAtom);
      });
    });
  }

  ngOnInit(): void {}

  async login(usernameOrEmail: string, password: string) {
    this.showSpinner(SpinnerType.BallAtom);
    await this.userAuthService.login(usernameOrEmail, password, () => {
      this.authService.idendtityCheck();
      this.activatedRoute.queryParams.subscribe((params) => {
        const returnUrl: string = params['returnUrl'];
        if (returnUrl) this.router.navigate([returnUrl]);
      });
      this.hideSpinner(SpinnerType.BallAtom);
    });
  }
}
