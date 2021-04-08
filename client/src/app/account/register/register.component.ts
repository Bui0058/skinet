import { Component, OnInit } from '@angular/core';
import { AsyncValidator, AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errors: string[];

  constructor(private fb: FormBuilder, private accountService : AccountService,
              private router : Router) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      displayName: [null, [Validators.required]],
      email: [null, 
        [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')], //this is sycn validator
        [this.validateEmailNotTaken()] //this async validator only call if sync validator all pass.
      ],
      password: [null, [Validators.required]]
    });
  }

  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/shop');
    }, error => {
      console.log(error);
      this.errors = error.errors;
    })
  }

  validateEmailNotTaken() : AsyncValidatorFn {
    return control => { //control is the first observalbe
      return timer(500).pipe( //the timer is to give delay to making to api whne user typing
        switchMap(() => { //we want to return inner observable to outer observalbe which is control
          if(!control.value) { 
            return of(null); //return observable of somthing - this case null
          }
          return this.accountService.checkEmailExist(control.value).pipe( //control.value is the email
            map(res => {
              return res ? {emailExists: true} : null; //emailExists is the same as required or pattern in the validator
            })
          );
        })
      );
    }
  }
}
