import React, { useEffect, useState } from 'react';
import { CssTextField } from 'utils/CssTextField';
import * as Yup from 'yup';
import { minLengthPassword, routeLogin } from '../../../constants';
import { RegisterModel } from '../../../models/useridentity.model';
import AuthForm from './FormAuth';
import { useAppSelector } from '../../../app/hooks';
import { useRegisterMutation } from '../auth.service';
import { isEntityError } from 'helpers';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { FormError } from 'models/error.model';

interface RegisterProps {}

const initialValues: RegisterModel = {
  email: '',
  password: '',
  confirmPwd: '',
  fullName: '',
  phoneNumber: '',
};

const RegisterPage: React.FC = (props: RegisterProps): JSX.Element => {
  const navigate = useNavigate();
  const [register, registerResult] = useRegisterMutation();
  const [formError, setFormError] = useState<FormError>(null);

  useEffect(() => {
    document.title = 'Đăng ký tài khoản';

    if (registerResult.data) {
      toast.success('Đăng ký thành công!');
      navigate(`/${routeLogin}`);
    }
  }, [formError, navigate, registerResult]);

  const validationRegister = Yup.object({
    email: Yup.string().required('Vui lòng nhập email.').email('Vui lòng nhập một email hợp lệ.'),
    password: Yup.string()
      .required('Mật khẩu là bắt buộc.')
      .min(minLengthPassword, `Mật khẩu không được ít hơn ${minLengthPassword} ký tự`),
    fullName: Yup.string().required('Họ tên không được để trống.'),
    confirmPwd: Yup.string()
      .oneOf([Yup.ref('password'), undefined], 'Xác nhận mật khẩu không đúng')
      .required('Xác nhận mật khẩu là bắt buộc.'),
  });

  const handleSubmit = async (values: RegisterModel) => {
    try {
      setFormError(null);
      await register(values).unwrap();
    } catch (error) {
      if (isEntityError(error)) {
        const errorResult = error.data.error as FormError;
        setFormError(errorResult);
      }
    }
  };

  // const registerSubmit = async (userRegister: RegisterModel) => {
  // 	console.log("Register submit values: ", userRegister);

  // 	const data = new FormData();
  // 	data.append('email', userRegister.email);
  // 	data.append('password', userRegister.password);
  // 	data.append('fullName', userRegister.fullName);
  // 	data.append('confirmPwd', userRegister.confirmPassword);

  // 	if (userRegister.avatar) {
  // 		data.append('avatar', userRegister.avatar, userRegister.avatar.name);
  // 	}
  // 	console.log(data);
  // 	await axiosClient.post("/account/register", data, {
  // 		headers: {
  // 			'Content-Type': 'multipart/form-data'
  // 		}
  // 	}).then(response => {
  // 		if (typeof (response.data) === 'string') {
  // 			Swal.fire({
  // 				icon: 'warning',
  // 				title: response.data,
  // 			});
  // 		}
  // 		if (typeof (response.data) === 'boolean') {
  // 			if (response.data === true) {
  // 				Swal.fire({
  // 					icon: 'success',
  // 					title: 'Đăng ký thành công!',
  // 					confirmButtonText: 'Đăng nhập'
  // 				}).then(result => {
  // 					if (result.isConfirmed) {
  // 						navigate(`/${routeLogin}`);
  // 					}
  // 				});
  // 			}
  // 		}
  // 	}).catch(error => console.log(error));
  // }

  return (
    <AuthForm<RegisterModel>
      title="ĐĂNG KÝ"
      titleUrl="Bạn đã có tài khoản? Đăng nhập ngay!"
      urlLink={routeLogin}
      model={initialValues}
      onSubmit={handleSubmit}
      validationSchema={validationRegister}
      isLoading={registerResult.isLoading}
    >
      {(formikProps) => (
        <>
          <CssTextField
            margin="normal"
            fullWidth
            id="fullName"
            name="fullName"
            label="Họ tên"
            placeholder="Nhập họ và tên"
            value={formikProps.values.fullName}
            onChange={formikProps.handleChange}
            error={formikProps.touched.fullName && Boolean(formikProps.errors.fullName)}
            helperText={formikProps.touched.fullName && formikProps.errors.fullName}
          />
          <CssTextField
            margin="normal"
            fullWidth
            id="email"
            type="email"
            name="email"
            label="Email"
            placeholder="Nhập email"
            value={formikProps.values.email}
            onChange={formikProps.handleChange}
            error={
              (formikProps.touched.email && Boolean(formikProps.errors.email)) ||
              Boolean(formError?.email)
            }
            helperText={
              (formikProps.touched.email && formikProps.errors.email) ||
              (Boolean(formError?.email) && formError?.email)
            }
          />
          <CssTextField
            margin="normal"
            fullWidth
            id="password"
            type="password"
            name="password"
            label="Mật khẩu"
            placeholder="Nhập mật khẩu"
            value={formikProps.values.password}
            onChange={formikProps.handleChange}
            error={
              (formikProps.touched.password && Boolean(formikProps.errors.password)) ||
              Boolean(formError?.password)
            }
            helperText={
              (formikProps.touched.password && formikProps.errors.password) ||
              (Boolean(formError?.password) && formError?.password)
            }
          />
          <CssTextField
            margin="normal"
            fullWidth
            id="confirmPwd"
            type="password"
            name="confirmPwd"
            label="Nhập lại mật khẩu"
            placeholder="Nhập lại mật khẩu"
            value={formikProps.values.confirmPwd}
            onChange={formikProps.handleChange}
            error={
              (formikProps.touched.confirmPwd && Boolean(formikProps.errors.confirmPwd)) ||
              Boolean(formError?.confirmPwd)
            }
            helperText={
              (formikProps.touched.confirmPwd && formikProps.errors.confirmPwd) ||
              (Boolean(formError?.confirmPwd) && formError?.confirmPwd)
            }
          />
        </>
      )}
    </AuthForm>
  );
};

export default React.memo(RegisterPage);
