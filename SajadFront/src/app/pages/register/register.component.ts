import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { JWTTokenService } from 'src/app/services/jwttoken.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public loginForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  });

  public get userName(): AbstractControl { return this.loginForm.get('userName') ?? new FormControl(); }
  public get password(): AbstractControl { return this.loginForm.get('password')  ?? new FormControl(); }
  public showLoading = false;
  public hidePassword = true;

  constructor(
    private service: AuthenticationService,
    private jwtTokenService: JWTTokenService,
    private snackBar: MatSnackBar,
    protected router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {

  }

  public async login() {
    if (this.loginForm.valid) {

      const value = this.loginForm.value;

      try {
        this.showLoading = true;
        await this.service.register(value);

        this.router.navigate(['Admin']);
      } finally {
        this.showLoading = false;
      }
    }
  }

}
