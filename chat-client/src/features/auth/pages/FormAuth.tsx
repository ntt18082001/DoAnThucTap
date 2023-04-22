import { Form, Formik, FormikProps, FormikValues } from 'formik';
import React, { ReactElement } from 'react';
import { CssLoadingButton } from 'utils/CssTextField';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';

import FormLayout from '../../../components/Layout/FormLayout';

interface AuthFormProps<TModel> {
	title: string;
	titleUrl: string;
	urlLink: string;
	model: TModel;
	onSubmit(values: TModel): void;
	validationSchema: any;
	children(formikProps: FormikProps<TModel>): ReactElement;
	isLoading?: boolean;
}

export default function AuthForm<TModel extends FormikValues>(props: AuthFormProps<TModel>) {
	return (
		<FormLayout
			title={props.title}
			titleUrl={props.titleUrl}
			urlLink={props.urlLink}
		>
			<Formik<TModel>
				initialValues={props.model}
				onSubmit={(values, actions) => {
					props.onSubmit(values);
				}}
				validationSchema={props.validationSchema}
			>
				{formikProps => (
					<Form
						autoComplete='off'
						style={{ marginTop: 2 }}
					>
						{props.children(formikProps)}
						<CssLoadingButton
							color="secondary"
							loadingPosition="start"
							startIcon={<AccountCircleIcon />}
							variant="contained"
							fullWidth
							loading={props.isLoading}
							type="submit"
							sx={{ mt: 3, mb: 2 }}
						>
							{props.title}
						</CssLoadingButton>
					</Form>
				)}
			</Formik>
		</FormLayout>
	);
}
