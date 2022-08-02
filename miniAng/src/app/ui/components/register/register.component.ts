import { ToastrService } from 'ngx-toastr';
import { Create_User } from './../../../contracts/users/create_user';
import { UserService } from './../../../services/common/model/user.service';
import { User } from './../../../entities/user';
import { group } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private toastrService: ToastrService
  ) {}

  registerForm: FormGroup;

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group(
      {
        fullName: [
          '',
          [
            Validators.required,
            Validators.maxLength(50),
            Validators.minLength(3),
          ],
        ],
        username: ['', [Validators.required]],
        email: ['', [Validators.required]],
        password: ['', [Validators.required]],
        rePassword: ['', [Validators.required]],
      },
      {
        validators: (group: AbstractControl): ValidationErrors | null => {
          let password = group.get('password').value;
          let rePassword = group.get('rePassword').value;
          return password === rePassword ? null : { notSame: true };
        },
      }
    );
  }

  get component() {
    return this.registerForm.controls;
  }

  submitted: boolean = false;

  async onSubmit(user: User) {
    this.submitted = true;

    if (this.registerForm.invalid) return;

    const result: Create_User = await this.userService.create(user);

    if (result.succeeded) {
      this.toastrService.success(result.message);
    } else this.toastrService.error(result.message);
  }
}
