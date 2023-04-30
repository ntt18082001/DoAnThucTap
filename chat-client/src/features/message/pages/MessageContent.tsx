import { Grid } from "@mui/material";
import { useAppSelector } from "app/hooks";
import { selectIsDarkmode } from "features/darkmode/darkmodeSlice";
import React, { useEffect } from "react";
import { borderColorDarkmode, borderColorDefault, routeMessage } from '../../../constants/index';
import Conversation from "./Conversation/Conversation";
import { useAppDispatch } from '../../../app/hooks';
import { useNavigate, useParams } from "react-router-dom";
import { selectSelectedUser, setSelectedUser } from '../messageSlice';
import { useGetUserSelectedQuery } from "../message.service";

type Props = {};

const MessageContent = (props: Props) => {
	const isDarkmode = useAppSelector(selectIsDarkmode);
	const navigate = useNavigate();

	const dispatch = useAppDispatch();
	const { id } = useParams();
	const { data, isSuccess } = useGetUserSelectedQuery(id != null ? id : '');
	const selectedUser = useAppSelector(selectSelectedUser);

	useEffect(() => {
		if(isSuccess && data == null) {
			navigate(routeMessage);
		}
		if (!selectedUser && data && isSuccess) {
			dispatch(setSelectedUser(data));
		}
	}, [selectedUser, dispatch, id, data, isSuccess, navigate]);

	const borderColor = isDarkmode ? borderColorDarkmode : borderColorDefault;

	return (
		<Grid
			item
			xs={7}
			sm={7}
			md={8}
			lg={9}
      xl={10}
			sx={{ borderRight: borderColor }}
		>
			<Conversation />
		</Grid>
	);
};

export default React.memo(MessageContent);
