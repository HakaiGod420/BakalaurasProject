export interface LoginData {
  userName: string;
  password: string;
}

export interface RegisterData {
  userName: string;
  email: string;
  password: string;
  rePassword: string;
}

export interface UserAddress {
  Country: string;
  City: string;
  StreetName: string;
  Province: string;
  Map_X_Cords: number | undefined;
  Map_Y_Cords: number | undefined;
}

export interface UserSettings {
  Address: UserAddress;
  EnabledInvitationSettings: boolean;
}

export interface AddressEdit {
  Address: UserAddress | undefined;
  EnabledInvitationSettings: boolean | undefined;
}

export interface Notification {
  IsActive: boolean | undefined;
  Title: string;
}

export interface NotificationSettings {
  Notifications: Notification[];
}

export interface UserInformation {
  UserId: number;
  UserName: string;
  Role: string;
  InvititationsCreated: number;
  TableTopGamesCreated: number;
  State: string;
  RegisteredOn: string;
  LastLogin: string;
}
