import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { retry } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  url = "";
  constructor(private http: HttpClient, private router: Router) {
    this.url = environment.api;
  }

  post(endpoint: string, body: any): Observable<any> {

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post(this.url + "" + endpoint, body, httpOptions).pipe(retry(2));
  }

  get(endpoint: string, params?: any, reqOpts?: any): Observable<any> {

    if (!reqOpts) {
      reqOpts = {
        params: new HttpParams()
      };
    }

    if (params) {
      reqOpts.params = new HttpParams();
      for (let k in params) {
        reqOpts.params.set(k, params[k]);
      }
    }

    reqOpts = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    return this.http.get(this.url + "" + endpoint, reqOpts).pipe(retry(3));
  }

}
