import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { CssTextField } from 'utils/CssTextField';
import * as Yup from 'yup';

import { useAppDispatch } from '../../../app/hooks';
import { minLengthPassword, routeRegister } from '../../../constants';
import { LoginModel } from '../../../models/useridentity.model';
import AuthForm from './FormAuth';
import { useLoginMutation } from '../auth.service';
import { FormError } from 'models/error.model';
import { isEntityError } from 'helpers';
import { setToken, setUser } from '../authSlice';
import { toast } from 'react-toastify';

interface LoginProps {
}

const LoginPage: React.FC = (props: LoginProps): JSX.Element => {
	const dispatch = useAppDispatch();
	const navigate = useNavigate();
	const [login, { data, isLoading, isSuccess }] = useLoginMutation();
	const [formError, setFormError] = useState<FormError>(null);

	const initialValue: LoginModel = {
		email: '',
		password: ''
	}

	useEffect(() => {
		document.title = "Đăng nhập";

		if(isSuccess) {
			if(data && data.token && data.user) {
				dispatch(setToken(data.token));
				dispatch(setUser(data.user));
			}
			toast.success("Đăng nhập thành công!");
			navigate("/");
		}
	}, [isSuccess, data, dispatch, navigate]);

	const handleSubmit = async (values: LoginModel) => {
		try {
			setFormError(null);
			await login(values).unwrap();
		} catch (error) {
			if(isEntityError(error)) {
				const errorResult = error.data.error as FormError;
				setFormError(errorResult);
			}
		}
	}

	const validationLogin = Yup.object({
		email: Yup.string().required("Vui lòng nhập email.")
			.email("Vui lòng nhập một email hợp lệ."),
		password: Yup.string().required("Mật khẩu là bắt buộc.")
			.min(minLengthPassword, `Mật khẩu không được ít hơn ${minLengthPassword} ký tự`)
	});

	return (
		<AuthForm<LoginModel>
			title="ĐĂNG NHẬP"
			titleUrl="Bạn chưa có tài khoản? Đăng ký ngay!"
			urlLink={routeRegister}
			model={initialValue}
			onSubmit={handleSubmit}
			validationSchema={validationLogin}
			isLoading={isLoading}
		>
			{(formikProps) => (
				<>
					<CssTextField
						margin="normal"
						id="email"
						type="email"
						name="email"
						label="Email"
						placeholder="Nhập email"
						value={formikProps.values.email}
						onChange={formikProps.handleChange}
						error={(formikProps.touched.email && Boolean(formikProps.errors.email)) || Boolean(formError?.email)}
						helperText={(formikProps.touched.email && formikProps.errors.email) || (Boolean(formError?.email) && formError?.email)}
						fullWidth
					/>
					<CssTextField
						margin="normal"
						id="password"
						type="password"
						name="password"
						label="Mật khẩu"
						placeholder="Nhập mật khẩu"
						value={formikProps.values.password}
						onChange={formikProps.handleChange}
						error={(formikProps.touched.password && Boolean(formikProps.errors.password)) || Boolean(formError?.password)}
						helperText={(formikProps.touched.password && formikProps.errors.password) || (Boolean(formError?.password) && formError?.password)}
						fullWidth
					/>
				</>
			)}
		</AuthForm>
	);
}

export default React.memo(LoginPage);