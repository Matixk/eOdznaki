import {MyLocation} from './myLocation';

export class Trail {
  constructor(
    public startPoint: {latitude: number, longitude: number},
    public endPoint: MyLocation,
    public checkpoints: MyLocation[]
  ) {}
}
