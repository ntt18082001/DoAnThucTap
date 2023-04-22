import { Form, Formik } from 'formik';
import { UserModel } from '../../../models/useridentity.model';
import { CssLoadingButton, CssTextField } from 'utils/CssTextField';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import ClearIcon from '@mui/icons-material/Clear';
import { Button } from '@mui/material';
import * as Yup from 'yup';
import React from 'react';

interface UserFormProps {
  model: UserModel;
  onSubmit(values: UserModel): void;
  isLoading?: boolean;
}

function FormUser(props: UserFormProps) {
  const validation = Yup.object({
    fullName: Yup.string().required('Họ và tên không được trống!'),
    phoneNumber: Yup.string().nullable().matches(/^[0-9]{10}$/g, 'Số điện thoại không hợp lệ!'),
  });

  return (
    <>
      <Formik<UserModel>
        initialValues={props.model}
        onSubmit={(values, actions) => {
          props.onSubmit(values);
        }}
        validationSchema={validation}
      >
        {(formikProps) => (
          <Form autoComplete="off" style={{ marginTop: 2 }}>
            <CssTextField
              margin="normal"
              id="fullName"
              type="fullName"
              name="fullName"
              label="Họ tên"
              placeholder="Nhập họ tên"
              value={formikProps.values.fullName || ''}
              onChange={formikProps.handleChange}
              error={formikProps.touched.fullName && Boolean(formikProps.errors.fullName)}
              helperText={formikProps.touched.fullName && formikProps.errors.fullName}
              fullWidth
            />
            <CssTextField
              margin="normal"
              id="phoneNumber"
              type="phoneNumber"
              name="phoneNumber"
              label="Số điện thoại"
              placeholder="Nhập số điện thoại"
              value={formikProps.values.phoneNumber || ''}
              onChange={formikProps.handleChange}
              error={formikProps.touched.phoneNumber && Boolean(formikProps.errors.phoneNumber)}
              helperText={formikProps.touched.phoneNumber && formikProps.errors.phoneNumber}
              fullWidth
            />
            <CssTextField
              margin="normal"
              id="address"
              type="address"
              name="address"
              label="Địa chỉ"
              placeholder="Nhập địa chỉ"
              value={formikProps.values.address || ''}
              onChange={formikProps.handleChange}
              fullWidth
            />
            <CssLoadingButton
              color="secondary"
              loadingPosition="start"
              startIcon={<AccountCircleIcon />}
              variant="contained"
              loading={props.isLoading}
              type="submit"
              sx={{ mt: 3, mb: 2, mr: 2 }}
            >
              Lưu
            </CssLoadingButton>
            <Button
              color="info"
              variant="contained"
              type="reset"
              sx={{ mt: 3, mb: 2 }}
              startIcon={<ClearIcon />}
            >
              Hủy
            </Button>
          </Form>
        )}
      </Formik>
    </>
  );
}

export default React.memo(FormUser);