import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { catchError, delay } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private toastr: ToastrService) {}
  // next is the http resonse from API
  //pipe & then catchError of rxjs then do whatever we want with error
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      delay(1000), //add delay 
      catchError(error => {
          if (error) {
            if (error.status === 400) {
              if (error.error.errors) {
                throw error.error;                
              } else {
                this.toastr.error(error.error.message, error.error.statusCode);            
              }              
            }
            if (error.status === 401) {
              this.toastr.error(error.error.message, error.error.statusCode);              
            }
            if (error.status === 404) {
              this.router.navigateByUrl('/not-found');
            }
            if (error.status === 500) {
              //this state will pass to component by router
              const navigationExtras: NavigationExtras = {state: {error: error.error}};
              this.router.navigateByUrl('/server-error', navigationExtras);
            }            
          }
          return  throwError(error);
      })
    );
  }
}
