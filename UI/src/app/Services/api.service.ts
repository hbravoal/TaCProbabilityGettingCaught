import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  UploadFile(formData: any) {
    return  this.http.get<any>(environment.baseUrl
      + `${environment.ProxyUpload}`, formData);
    }
}
