import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from './../../../services/common/model/user.service';
import { Component, OnInit } from '@angular/core';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends BaseComponent implements OnInit {
  constructor(private userService: UserService, spinner: NgxSpinnerService) {
    super(spinner);
  }

  ngOnInit(): void {}

  async login(usernameOrEmail: string, password: string) {
    this.showSpinner(SpinnerType.BallAtom);

    await this.userService.login(usernameOrEmail, password, () =>
      this.hideSpinner(SpinnerType.BallAtom)
    );
  }
}
