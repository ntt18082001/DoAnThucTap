export interface LoginModel {
	email: string;
	password: string;
}

export interface RegisterModel {
	email: string;
	password: string;
	confirmPwd: string;
	avatar?: File;
	fullName: string;
	phoneNumber?: string;
}

export interface UserModel {
	id: string;
	email: string;
	avatar: string;
	fullName: string;
	address?: string;
	phoneNumber?: string;
}

export interface AuthModel {
	token: string | null;
	user: UserModel | null;
	isLoggedIn: boolean;
}

export interface claim {
	name: string;
	value: string;
}

export interface LoginResponse {
	token?: string;
	user?: UserModel;
}

export interface Auth {
	token: string | null;
	user: UserModel | null;
}

export interface AvatarResponse {
	avatar: string;
}

export interface ChangePasswordModel {
	id: string;
	password: string;
	newPassword: string;
	confirmNewPassword: string;
}

export interface ForgotPasswordModel {
	email: string;
}

export interface VerifyCodeModel {
	code: string;
}

export interface NewPassword {
	email: string;
	code: string;
	password: string;
	confirmPwd: string;
}