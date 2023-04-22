import {
  Button,
  Checkbox,
  Dialog,
  DialogContent,
  DialogTitle,
  FormControlLabel,
} from '@mui/material';
import { useAppDispatch } from 'app/hooks';
import { minLengthPassword } from '../../../constants';
import React, { useEffect, useState } from 'react';
import * as Yup from 'yup';
import ClearIcon from '@mui/icons-material/Clear';
import { CssLoadingButton, CssTextField } from 'utils/CssTextField';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { Form, Formik } from 'formik';
import { ChangePasswordModel } from 'models/useridentity.model';
import { FormError } from 'models/error.model';
import { useChangePasswordMutation } from '../profile.service';
import { isEntityError } from 'helpers';
import { toast } from 'react-toastify';
import { logout } from 'features/auth/authSlice';

interface ChangePasswordProps {
  isOpen: boolean;
  id?: string;
  handleClose: () => void;
}

const initialState: ChangePasswordModel = {
  id: '',
  password: '',
  newPassword: '',
  confirmNewPassword: '',
};

function FormChangePassword(props: ChangePasswordProps) {
  const dispatch = useAppDispatch();
  const [formError, setFormError] = useState<FormError>(null);
  const [isLogout, setIsLogout] = useState(true);
  const [changePassword, { data, isSuccess, isLoading, reset }] = useChangePasswordMutation();

  const handleSubmit = async (values: ChangePasswordModel) => {
    try {
			setFormError(null);
      if (props.id) {
        values.id = props.id;
      }
      await changePassword(values).unwrap();
      props.handleClose();
    } catch (error) {
      if (isEntityError(error)) {
        const errorResult = error.data.error as FormError;
        setFormError(errorResult);
      }
    }
  };

  useEffect(() => {
    if (isSuccess) {
      if (data) {
        toast.success('Đổi mật khẩu thành công!');
        if (isLogout) {
          dispatch(logout());
        }
        reset();
      }
    }
  }, [isSuccess, data, isLogout, dispatch, reset]);

  const handleChecked = (event: React.ChangeEvent<HTMLInputElement>) => {
    setIsLogout(event.target.checked);
  };

  const validationChangePwd = Yup.object({
    password: Yup.string()
      .required('Mật khẩu là bắt buộc.')
      .min(minLengthPassword, `Mật khẩu không được ít hơn ${minLengthPassword} ký tự`),
    newPassword: Yup.string()
      .required('Mật khẩu mới là bắt buộc.')
      .min(minLengthPassword, `Mật khẩu không được ít hơn ${minLengthPassword} ký tự`),
    confirmNewPassword: Yup.string()
      .oneOf([Yup.ref('newPassword'), undefined], 'Xác nhận mật khẩu không đúng')
      .required('Xác nhận mật khẩu là bắt buộc.'),
  });

  return (
    <div>
      <Dialog
        open={props.isOpen}
        onClose={props.handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">Đổi mật khẩu</DialogTitle>
        <DialogContent>
          <Formik<ChangePasswordModel>
            initialValues={initialState}
            onSubmit={(values, actions) => {
              handleSubmit(values);
              if(isSuccess && data) {
                actions.resetForm({
                  values: {
                    id: '',
                    password: '',
                    newPassword: '',
                    confirmNewPassword: ''
                  }
                });
              }
            }}
            validationSchema={validationChangePwd}
          >
            {(formikProps) => (
              <Form autoComplete="off" style={{ marginTop: 2 }}>
                <CssTextField
                  margin="normal"
                  fullWidth
                  id="password"
                  type="password"
                  name="password"
                  label="Mật khẩu cũ"
                  placeholder="Nhập mật khẩu cũ"
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
                  id="newPassword"
                  type="password"
                  name="newPassword"
                  label="Mật khẩu mới"
                  placeholder="Nhập mật khẩu mới"
                  value={formikProps.values.newPassword}
                  onChange={formikProps.handleChange}
                  error={
                    (formikProps.touched.newPassword && Boolean(formikProps.errors.newPassword)) ||
                    Boolean(formError?.newPassword)
                  }
                  helperText={
                    (formikProps.touched.newPassword && formikProps.errors.newPassword) ||
                    (Boolean(formError?.newPassword) && formError?.newPassword)
                  }
                />
                <CssTextField
                  margin="normal"
                  fullWidth
                  id="confirmNewPassword"
                  type="password"
                  name="confirmNewPassword"
                  label="Xác nhận mật khẩu mới"
                  placeholder="Xác nhận mật khẩu mới"
                  value={formikProps.values.confirmNewPassword}
                  onChange={formikProps.handleChange}
                  error={
                    (formikProps.touched.confirmNewPassword &&
                      Boolean(formikProps.errors.confirmNewPassword)) ||
                    Boolean(formError?.confirmNewPassword)
                  }
                  helperText={
                    (formikProps.touched.confirmNewPassword &&
                      formikProps.errors.confirmNewPassword) ||
                    (Boolean(formError?.confirmNewPassword) && formError?.confirmNewPassword)
                  }
                />
                <FormControlLabel
                  control={<Checkbox checked={isLogout} onChange={handleChecked} />}
                  label="Đăng xuất sau khi đổi mật khẩu"
                />
                <br />
                <CssLoadingButton
                  color="secondary"
                  loadingPosition="start"
                  startIcon={<AccountCircleIcon />}
                  variant="contained"
                  loading={isLoading}
                  type="submit"
                  sx={{ mt: 3, mb: 2, mr: 2 }}
                >
                  Lưu
                </CssLoadingButton>
                <Button
                  color="info"
                  variant="contained"
                  sx={{ mt: 3, mb: 2 }}
                  startIcon={<ClearIcon />}
                  onClick={() => {
                    formikProps.resetForm();
                    props.handleClose();
                  }}
                >
                  Đóng
                </Button>
              </Form>
            )}
          </Formik>
        </DialogContent>
      </Dialog>
    </div>
  );
}

export default React.memo(FormChangePassword);
