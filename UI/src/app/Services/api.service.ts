import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { TestFormRequest } from '../Models/test-form-model';
@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }
   headers = new Headers();

  UploadFile(testFormRequest: any) {
    this.headers.append('Access-Control-Allow-Headers', 'Content-Type');
    this.headers.append('Access-Control-Allow-Methods', 'POST');
    this.headers.append('Access-Control-Allow-Origin', '*');
    return  this.http.post<any>(environment.baseUrl
      + `${environment.ProxyUpload}`, testFormRequest, {
        reportProgress: true,
      });
    }
}
