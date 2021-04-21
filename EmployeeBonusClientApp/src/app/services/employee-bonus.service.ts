import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Employee } from '../models/employee';
import { CalculateBonusRequest } from './calculate-bonus-request';
import { CalculateBonusResponse } from './calculate-bonus-response';

@Injectable({
  providedIn: 'root'
})
export class EmployeeBonusService {
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private http: HttpClient) {
    this.myApiUrl = environment.apiUrl;
  }

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.myApiUrl+ 'BonusPool/Employees')
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  getEmployee(employeeId: number): Observable<Employee> {
    return this.http.get<Employee>(this.myApiUrl + 'BonusPool/Employee?id=' + employeeId)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  calculateBonus(selectedEmployeeId: number, totalBonusPoolAmount: number): Observable<CalculateBonusResponse> {
    const request = new CalculateBonusRequest();
    request.selectedEmployeeId = selectedEmployeeId;
    request.totalBonusPoolAmount = totalBonusPoolAmount;
    return this.http.post<CalculateBonusResponse>(this.myApiUrl+ 'BonusPool', request, this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  errorHandler(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}\nCheck request parameters like employeeId and try again`;
    }
    console.log(errorMessage);    
    return throwError(errorMessage);
  }

}
